using System;
using MusicCollection.DL.Cache;
using MusicCollection.Models.DTO;

namespace MusicCollection.DL.Interfaces;

public interface IMusicRepository : ICacheRepository<string, SongDTO>
{
    Task<List<SongDTO>> GetAllSongs();

    Task AddSong(SongDTO song);

    Task DeleteSong(string Id);

    Task<SongDTO?> GetSongById(string Id);

    Task UpdateSong(string Id, SongDTO song);

}

