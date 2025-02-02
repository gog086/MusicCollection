using System;

namespace MusicCollection.Models.Models;

public class Song
{
    public string Id { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public List<string> Platforms { get; set; } = [];
    
}
