using System;

namespace MusicCollection.Models.DTO;

public interface ICacheItem<T>
{
    public abstract DateTime DateInserted { get; set; }

    public abstract T GetKey();
}
