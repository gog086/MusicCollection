using System;

namespace MusicCollection.Models.DTO;

public class SongDTO
{
    public string Id { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public List<string> Platforms { get; set; } = [];
    
}
