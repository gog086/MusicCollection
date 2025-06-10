using System;
using MusicCollection.Models.DTO;

namespace MusicCollection.BL.Interfaces;

public interface IMusicService
{
    Task<List<SongDTO>> GetAllSongs();

    Task AddSong(SongDTO song);

    Task DeleteSong(string Id);

    Task<SongDTO?> GetSongById(string Id);

    Task UpdateSong(string Id, SongDTO song);

}
