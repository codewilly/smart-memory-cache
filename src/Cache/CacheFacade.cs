using Cache.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace Cache
{
    public class CacheFacade(IMemoryCache memoryCache) : ICacheFacade
    {
        private readonly IMemoryCache _memoryCache = memoryCache;

        public T? Get<T>(ICacheable<T> cacheable)
        {
            if (_memoryCache.TryGetValue(cacheable.GetHashKey(), out T? data))
                return data;

            return cacheable.GetDefaultValue();
        }

        public T Create<T>(ICacheable<T> cacheable, T data, int? expiration = null, int? slidingExpiration = null)
        {
            MemoryCacheEntryOptions opt = new()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(expiration ??  30),
                SlidingExpiration = TimeSpan.FromMinutes(slidingExpiration ?? 5)
            };

            _memoryCache.Set(cacheable.GetHashKey(), data, opt);

            return data;
        }

        public void Delete<T>(ICacheable<T> cacheable)
        {
            _memoryCache.Remove(cacheable.GetHashKey());
        }        
    }
}
