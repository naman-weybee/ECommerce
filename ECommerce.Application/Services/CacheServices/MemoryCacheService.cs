using ECommerce.Application.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace ECommerce.Application.Services.CacheServices
{
    public class MemoryCacheService : ICacheService
    {
        private readonly IMemoryCache _cache;

        public MemoryCacheService(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        public async Task<T?> GetOrCreateAsync<T>(string key, Func<Task<T>> factory, TimeSpan? absoluteExpiration = null)
        {
            if (_cache.TryGetValue<T>(key, out var cached))
                return cached;

            var result = await factory();

            var cacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = absoluteExpiration ?? TimeSpan.FromMinutes(30)
            };

            _cache.Set(key, result, cacheEntryOptions);

            return result;
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }
    }
}