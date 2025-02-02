using System;
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

    public List<SongDTO> GetAllSongs(){
        return _songRepository.GetAllSongs();
    }

    public void AddSong(SongDTO song){
        _songRepository.AddSong(song);           
    }

    public void DeleteSong(string Id){
        _songRepository.DeleteSong(Id);
    }

    public SongDTO? GetSongById(string Id){
        return _songRepository.GetSongById(Id);
    }

    public void UpdateSong(string Id, SongDTO song){
        _songRepository.UpdateSong(Id, song);
    }

}
