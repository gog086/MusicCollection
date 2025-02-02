using MusicCollection.BL.Interfaces;
using MusicCollection.DL.Interfaces;
using MusicCollection.Models.DTO;

namespace MusicCollection.BL.Services;

public class PlatformService : IPlatformService
{
    private readonly IPlatformRepository _platformRepository;

    public PlatformService(IPlatformRepository platformRepository){
        _platformRepository = platformRepository;

    }

    public void AddPlatform(PlatformDTO platform){
           _platformRepository.AddPlatform(platform);
    }

    public void DeletePlatform(string Id){
        _platformRepository.DeletePlatform(Id);
    }

    public IEnumerable<PlatformDTO> GetPlatformsByName(IEnumerable<string> platformNames){
        return _platformRepository.GetPlatformsByName(platformNames);
    }

    public PlatformDTO? GetPlatformById(string Id){
        return _platformRepository.GetPlatformById(Id);
    }
}
