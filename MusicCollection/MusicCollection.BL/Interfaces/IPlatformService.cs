using System;
using MusicCollection.Models.DTO;

namespace MusicCollection.BL.Interfaces;

public interface IPlatformService
{
    Task AddPlatform(PlatformDTO platform);

    Task DeletePlatform(string Id);

    Task<IEnumerable<PlatformDTO>> GetPlatformsByName(IEnumerable<string> platformNames);

    Task<PlatformDTO?> GetPlatformById(string Id);
}
