using MusicCollection.Models.Views;

namespace MusicCollection.BL.Interfaces;

public interface IMusicBlService
{
    Task<List<SongView>> GetDetailedSongs();
}
