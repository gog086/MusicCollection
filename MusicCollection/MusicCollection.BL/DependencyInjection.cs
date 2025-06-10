using MusicCollection.DL;
using Microsoft.Extensions.DependencyInjection;
using MusicCollection.BL.Interfaces;
using MusicCollection.BL.Services;


namespace MusicCollection.BL;

public static class DependencyInjection
{
     public static IServiceCollection RegisterBusinessLayer(this IServiceCollection services)
    {
        services.AddSingleton<IMusicService, MusicService>();
        services.AddSingleton<IPlatformService, PlatformService>();
        services.AddSingleton<IMusicBlService, MusicBlService>();
        services.AddHostedService<RandomSongAPIService>();
        return services;
    }

    public static IServiceCollection RegisterDataLayer(this IServiceCollection services)
    {

        return services;
    }



}
