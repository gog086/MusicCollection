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

    public void AddPlatform(PlatformDTO platform){
        if (platform == null)
        {
            _logger.LogError("Platform is null");
            return;
        }

        try
        {
            platform.Id = Guid.NewGuid().ToString();

            _platforms.InsertOne(platform);
        }
        catch (Exception e)
        {
            _logger.LogError(e,
               $"Error adding platform {e.Message}-{e.StackTrace}");
        }
           
    }

    public void DeletePlatform(string Id){
        _platforms.DeleteOne(platform => platform.Id == Id);
    }

    public IEnumerable<PlatformDTO> GetPlatformsByName(IEnumerable<string> platformNames){
        var result = _platforms.Find(platform => platformNames.Contains(platform.Name)).ToList();
        return result;
    }

    public PlatformDTO? GetPlatformById(string id){
        if (string.IsNullOrEmpty(id)) return null;

        return _platforms.Find(m => m.Id == id).FirstOrDefault();
    }
}
