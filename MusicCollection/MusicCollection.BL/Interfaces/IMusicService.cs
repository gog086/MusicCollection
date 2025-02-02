using System;
using MusicCollection.Models.DTO;

namespace MusicCollection.BL.Interfaces;

public interface IMusicService
{
    List<SongDTO> GetAllSongs();

    void AddSong(SongDTO song);

    void DeleteSong(string Id);

    SongDTO? GetSongById(string Id);

    void UpdateSong(string Id, SongDTO song);

}
