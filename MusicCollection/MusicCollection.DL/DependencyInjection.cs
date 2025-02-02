using System;
using Microsoft.Extensions.DependencyInjection;
using MusicCollection.DL;
using MusicCollection.DL.Interfaces;
using MusicCollection.DL.Repositories;

namespace MusicCollection.DL;

public static class DependancyInjection
{
    public static void RegisterRepositories(this IServiceCollection services)
    {
        services.AddSingleton<IMusicRepository, MusicRepository>();
        services.AddSingleton<IPlatformRepository, PlatformRepository>();
    }
}
