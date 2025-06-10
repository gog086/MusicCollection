using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MusicCollection.DL.Configurations;
using MusicCollection.DL.Interfaces;
using MusicCollection.Models.DTO;

namespace MusicCollection.DL.Repositories;

public class PlatformRepository : IPlatformRepository
{
    private readonly IMongoCollection<PlatformDTO> _platforms;
    private readonly ILogger<PlatformRepository> _logger;

    public PlatformRepository(IOptionsMonitor<MongoDbConfiguration> mongoConfig, ILogger<PlatformRepository> logger)
    {

        var client = new MongoClient(
            mongoConfig.CurrentValue.ConnectionString);

        var database = client.GetDatabase(
            mongoConfig.CurrentValue.DatabaseName);

        _platforms = database.GetCollection<PlatformDTO>("Platforms");

        _logger = logger;
    }

    public async Task AddPlatform(PlatformDTO platform)
    {
        if (platform == null)
        {
            _logger.LogError("Platform is null");
            return;
        }

        try
        {
            platform.Id = Guid.NewGuid().ToString();

            await _platforms.InsertOneAsync(platform);
        }
        catch (Exception e)
        {
            _logger.LogError(e,
               $"Error adding platform {e.Message}-{e.StackTrace}");
        }

    }

    public async Task DeletePlatform(string Id)
    {
        await _platforms.DeleteOneAsync(platform => platform.Id == Id);
    }

    public async Task<IEnumerable<PlatformDTO>> GetPlatformsByName(IEnumerable<string> platformNames)
    {
        var result = await _platforms.Find(platform => platformNames.Contains(platform.Name)).ToListAsync();
        return result;
    }

    public async Task<PlatformDTO?> GetPlatformById(string id)
    {
        if (string.IsNullOrEmpty(id)) return null;

        return await _platforms.Find(m => m.Id == id).FirstOrDefaultAsync();
    }
    
    public async Task<List<PlatformDTO>> GetAllPlatforms()
    {
        var platforms = await _platforms.FindAsync(platform => true);
        return await platforms.ToListAsync();
    }

    public async Task<IEnumerable<PlatformDTO?>> DifLoad(DateTime lastExecuted)
    {
        var result = await _platforms.FindAsync(m => m.DateInserted >= lastExecuted);

        return await result.ToListAsync();
    }

    public async Task<IEnumerable<PlatformDTO?>> FullLoad()
    {
        return await GetAllPlatforms();
    }
}
