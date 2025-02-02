using System;
using MusicCollection.Models.DTO;

namespace MusicCollection.DL.Interfaces;

public interface IMusicRepository
{
    List<SongDTO> GetAllSongs();

    void AddSong(SongDTO song);

    void DeleteSong(string Id);

    SongDTO? GetSongById(string Id);

    void UpdateSong(string Id, SongDTO song);

}

