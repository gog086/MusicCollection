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


    public List<SongDTO> GetAllSongs(){
        return _songs.Find(song => true).ToList();
    }

    public void AddSong(SongDTO song){
        if (song == null)
        {
            _logger.LogError("Song is null");
            return;
        }

        try
        {
            song.Id = Guid.NewGuid().ToString();

            _songs.InsertOne(song);
        }
        catch (Exception e)
        {
            _logger.LogError(e,
                   $"Error adding song {e.Message}-{e.StackTrace}");
        }
           
    }

    public void DeleteSong(string Id){
        _songs.DeleteOne(song => song.Id == Id);
    }

    public SongDTO GetSongById(string Id){
        return _songs.Find(song => song.Id == Id).FirstOrDefault();
    }

    public void UpdateSong(string Id, SongDTO updatedSongDto){
        var filter = Builders<SongDTO>.Filter.Eq(a => a.Id, Id);

        var update = Builders<SongDTO>.Update
            .Set(a => a.Name, updatedSongDto.Name)
            .Set(a => a.Platforms, updatedSongDto.Platforms);

        _songs.UpdateOne(filter, update);
    }
}
