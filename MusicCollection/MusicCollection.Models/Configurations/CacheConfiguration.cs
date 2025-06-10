using System;

namespace MusicCollection.Models.Configurations;

public abstract class CacheConfiguration
{
    public string Topic { get; set; } = string.Empty;

    public int RefreshInterval { get; set; } = 30;
}