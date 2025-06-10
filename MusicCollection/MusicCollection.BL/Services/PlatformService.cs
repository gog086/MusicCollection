using System.Threading.Tasks;
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

    public async Task AddPlatform(PlatformDTO platform){
           await _platformRepository.AddPlatform(platform);
    }

    public async Task DeletePlatform(string Id){
        await _platformRepository.DeletePlatform(Id);
    }

    public async Task<IEnumerable<PlatformDTO>> GetPlatformsByName(IEnumerable<string> platformNames){
        return await _platformRepository.GetPlatformsByName(platformNames);
    }

    public async Task<PlatformDTO?> GetPlatformById(string Id){
        return await _platformRepository.GetPlatformById(Id);
    }
}
