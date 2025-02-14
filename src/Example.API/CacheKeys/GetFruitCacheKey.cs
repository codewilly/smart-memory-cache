using Cache.Models;

namespace Example.API.CacheKeys
{
    public class GetFruitCacheKey(string fruit) : Cacheable<Guid?>
    {
        public string Fruit { get; set; } = fruit;
    }
}
