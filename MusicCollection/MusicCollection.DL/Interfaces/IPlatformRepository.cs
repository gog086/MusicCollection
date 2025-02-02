using System;
using MusicCollection.Models.DTO;
using MusicCollection.Models.Models;

namespace MusicCollection.DL.Interfaces;

public interface IPlatformRepository
{
    void AddPlatform(PlatformDTO platform);

    void DeletePlatform(string Id);

    IEnumerable<PlatformDTO> GetPlatformsByName(IEnumerable<string> platformNames);

    PlatformDTO? GetPlatformById(string Id);

}
