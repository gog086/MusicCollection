using MusicCollection.Models.Views;

namespace MusicCollection.BL.Interfaces;

public interface IMusicBlService
{
    List<SongView> GetDetailedSongs();
}
