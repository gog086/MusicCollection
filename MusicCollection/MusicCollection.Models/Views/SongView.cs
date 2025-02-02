using MusicCollection.Models.DTO;

namespace MusicCollection.Models.Views;

public class SongView
{
    public string SongId { get; set; } = string.Empty;

    public string SongName { get; set; } = string.Empty;

    public IEnumerable<PlatformDTO> Platforms { get; set; } = [];
    
}
