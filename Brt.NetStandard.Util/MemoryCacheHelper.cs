using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Brt.NetStandard.Util
{
    public class MemoryCacheHelper<TItem> : IMemoryCacheHelper<TItem>
    {
        private MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());
        private ConcurrentDictionary<object, SemaphoreSlim> _locks = new ConcurrentDictionary<object, SemaphoreSlim>();

        public async Task<TItem> GetOrCreateAsync(object key, Func<Task<TItem>> createItem, DateTime dateExpiration)
        {
            TItem cacheEntry;

            if (!_cache.TryGetValue(key, out cacheEntry))// Look for cache key.
            {
                SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));

                await mylock.WaitAsync();
                try
                {
                    var cacheOptions = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpiration = dateExpiration
                    };

                    if (!_cache.TryGetValue(key, out cacheEntry))
                    {
                        // Key not in cache, so get data.
                        cacheEntry = await createItem();
                        _cache.Set(key, cacheEntry, cacheOptions);
                    }
                }
                finally
                {
                    mylock.Release();
                }
            }

            return cacheEntry;
        }

        public async Task ClearCache(object key)
        {
            SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
            await mylock.WaitAsync();
            try
            {
                _cache.Remove(key);
            }
            finally
            {
                mylock.Release();
            }
        }
    }
}
