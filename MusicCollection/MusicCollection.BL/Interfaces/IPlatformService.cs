using System;
using MusicCollection.Models.DTO;

namespace MusicCollection.BL.Interfaces;

public interface IPlatformService
{
    void AddPlatform(PlatformDTO platform);

    void DeletePlatform(string Id);

    IEnumerable<PlatformDTO> GetPlatformsByName(IEnumerable<string> platformNames);

    PlatformDTO? GetPlatformById(string Id);
}
