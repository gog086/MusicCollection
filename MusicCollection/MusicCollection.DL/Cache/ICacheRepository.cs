using System;
using MusicCollection.Models.DTO;

namespace MusicCollection.DL.Cache;

public interface ICacheRepository<TKey, TData> where TData : ICacheItem<TKey>
{
    Task<IEnumerable<TData?>> FullLoad();

    Task<IEnumerable<TData?>> DifLoad(DateTime lastExecuted);
}
