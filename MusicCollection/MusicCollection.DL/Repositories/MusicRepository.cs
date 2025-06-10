using System;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MusicCollection.DL.Configurations;
using MusicCollection.DL.Interfaces;
using MusicCollection.Models.DTO;
using Microsoft.Extensions.Options;

namespace MusicCollection.DL.Repositories;

public class MusicRepository : IMusicRepository
{
    private readonly IMongoCollection<SongDTO> _songs;
    private readonly ILogger<MusicRepository> _logger;

    public MusicRepository(IOptionsMonitor<MongoDbConfiguration> mongoConfig, ILogger<MusicRepository> logger)
    {

        var client = new MongoClient(
            mongoConfig.CurrentValue.ConnectionString);

        var database = client.GetDatabase(
            mongoConfig.CurrentValue.DatabaseName);

        _songs = database.GetCollection<SongDTO>("Music");

        _logger = logger;
    }


    public async Task<List<SongDTO>> GetAllSongs()
    {
        var songs = await _songs.FindAsync(song => true);
        return await songs.ToListAsync();
    }

    public async Task AddSong(SongDTO song)
    {
        if (song == null)
        {
            _logger.LogError("Song is null");
            return;
        }

        try
        {
            song.Id = Guid.NewGuid().ToString();

            await _songs.InsertOneAsync(song);
        }
        catch (Exception e)
        {
            _logger.LogError(e,
                   $"Error adding song {e.Message}-{e.StackTrace}");
        }

    }

    public async Task DeleteSong(string Id)
    {
        await _songs.DeleteOneAsync(song => song.Id == Id);
    }

    public async Task<SongDTO?> GetSongById(string Id)
    {
        return await _songs.Find(song => song.Id == Id).FirstOrDefaultAsync();
    }

    public async Task UpdateSong(string Id, SongDTO updatedSongDto)
    {
        var filter = Builders<SongDTO>.Filter.Eq(a => a.Id, Id);

        var update = Builders<SongDTO>.Update
            .Set(a => a.Name, updatedSongDto.Name)
            .Set(a => a.Platforms, updatedSongDto.Platforms);

        await _songs.UpdateOneAsync(filter, update);
    }
    
    protected async Task<IEnumerable<SongDTO?>> GetSongsAfterDateTime(DateTime date)
    {
        var result = await _songs.FindAsync(m => m.DateInserted >= date);

        return await result.ToListAsync();
    }

    public async Task<IEnumerable<SongDTO?>> FullLoad()
    {
        return await GetAllSongs();
    }

    public async Task<IEnumerable<SongDTO?>> DifLoad(DateTime lastExecuted)
    {
        return await GetSongsAfterDateTime(lastExecuted);
    }
}
