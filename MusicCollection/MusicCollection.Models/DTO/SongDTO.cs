using System;
using MessagePack;

namespace MusicCollection.Models.DTO;

[MessagePackObject]
public class SongDTO : ICacheItem<string>
{
    [MessagePack.Key(0)]
    public string Id { get; set; } = string.Empty;
    [MessagePack.Key(1)]
    public string Name { get; set; } = string.Empty;

    [MessagePack.Key(2)]
    public List<string> Platforms { get; set; } = [];

    [MessagePack.Key(3)]
    public DateTime DateInserted { get;  set ; }

    public string GetKey()
    {
        return Id;
    }
}
