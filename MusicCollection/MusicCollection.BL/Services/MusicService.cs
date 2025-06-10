using System;
using System.Threading.Tasks;
using MusicCollection.BL.Interfaces;
using MusicCollection.DL.Interfaces;
using MusicCollection.Models.DTO;
using MusicCollection.Models.Models;

namespace MusicCollection.BL.Services;

public class MusicService : IMusicService
{
    private readonly IMusicRepository _songRepository;
    private readonly IPlatformRepository _platformRepository; 

    public MusicService(IMusicRepository songRepository, IPlatformRepository platformRepository){

        _songRepository = songRepository;
        _platformRepository = platformRepository;

    }

    public async Task<List<SongDTO>> GetAllSongs(){
        return await _songRepository.GetAllSongs();
    }

    public async Task AddSong(SongDTO song){
        await _songRepository.AddSong(song);           
    }

    public async Task DeleteSong(string Id){
        await _songRepository.DeleteSong(Id);
    }

    public async Task<SongDTO?> GetSongById(string Id){
        return await _songRepository.GetSongById(Id);
    }

    public async Task UpdateSong(string Id, SongDTO song){
        await _songRepository.UpdateSong(Id, song);
    }

}
