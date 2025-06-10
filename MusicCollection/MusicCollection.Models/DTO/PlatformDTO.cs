using System;
using System.ComponentModel.DataAnnotations;
using MessagePack;
using Serilog.Core;

namespace MusicCollection.Models.DTO;

[MessagePackObject]
public class PlatformDTO : ICacheItem<string>
{
    [MessagePack.Key(0)]
    public string Id { get; set; } = string.Empty;

    [MessagePack.Key(1)]
    public string Name { get; set; } = string.Empty;

    [MessagePack.Key(2)]
    public string URL { get; set; } = string.Empty;

    [MessagePack.Key(3)]
    public DateTime DateInserted { get; set; }

    public string GetKey()
    {
        return Id;
    }
}
