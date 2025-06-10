using System;

namespace MusicCollection.Models.Models;

public class Platform
{
    public string Id { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string URL { get; set; } = string.Empty;

    public DateTime DateInserted { get;  set ; }
    
}
