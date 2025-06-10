using System;
using MusicCollection.DL.Cache;
using MusicCollection.Models.DTO;
using MusicCollection.Models.Models;

namespace MusicCollection.DL.Interfaces;

public interface IPlatformRepository : ICacheRepository<string, PlatformDTO>
{
    Task AddPlatform(PlatformDTO platform);

    Task DeletePlatform(string Id);

    Task<IEnumerable<PlatformDTO>> GetPlatformsByName(IEnumerable<string> platformNames);

    Task<PlatformDTO?> GetPlatformById(string Id);

}
