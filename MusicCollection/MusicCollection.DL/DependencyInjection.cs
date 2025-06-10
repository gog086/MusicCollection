using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MusicCollection.DL;
using MusicCollection.DL.Cache;
using MusicCollection.DL.Interfaces;
using MusicCollection.DL.Kafka;
using MusicCollection.DL.Kafka.KafkaCache;
using MusicCollection.DL.Repositories;
using MusicCollection.Models.Configurations;
using MusicCollection.Models.DTO;

namespace MusicCollection.DL;

public static class DependancyInjection
{

    public static IServiceCollection
        AddDataDependencies(this IServiceCollection services, IConfiguration config)
    {
        services.AddSingleton<IMusicRepository, MusicRepository>();
        services.AddSingleton<IPlatformRepository, PlatformRepository>();
        
        services.AddCache<MusicCacheConfiguration, MusicRepository, SongDTO, string>(config);
        services.AddCache<PlatformCacheConfiguration, PlatformRepository, PlatformDTO, string>(config);
        
        services.AddHostedService<KafkaCache<string, SongDTO>>();
        
        return services;
    }
    
    public static IServiceCollection AddCache<TCacheConfiguration, TCacheRepository, TData, TKey>(this IServiceCollection services, IConfiguration config)
        where TCacheConfiguration : CacheConfiguration
        where TCacheRepository : class, ICacheRepository<TKey, TData>
        where TData : ICacheItem<TKey>
        where TKey : notnull
    {
        var configSection = config.GetSection(typeof(TCacheConfiguration).Name);

        if (!configSection.Exists())
        {
            throw new ArgumentNullException(typeof(TCacheConfiguration).Name, "Configuration section is missing in appsettings!");
        }

        services.Configure<TCacheConfiguration>(configSection);

        services.AddSingleton<ICacheRepository<TKey, TData>, TCacheRepository>();
        services.AddSingleton<IKafkaProducer<TData>, KafkaProducer<TKey, TData, TCacheConfiguration>>();
        
        services.AddHostedService<MongoCachePopulator<TData, TCacheConfiguration, TKey>>();

        return services;
    }
}
