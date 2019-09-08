using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;

namespace WeatherReport.WebApi.ClientApi
{
    public interface IMemoryCacheService : IMemoryCache
    {
        T Get<T>(string cacheKey) where T : class;
        void Set<T>(string cacheKey, T item, int minutes);
    }

    public class MemoryCacheService : MemoryCache, IMemoryCacheService
    {
        private readonly IMemoryCache _memoryCache;

        public T Get<T>(string cacheKey) where T : class
        {
            _memoryCache.TryGetValue(cacheKey, out T value);
            return value;
        }


        public void Set<T>(string cacheKey, T item, int minutes)
        {
            _memoryCache.Set(cacheKey, item, DateTime.Now.AddMinutes(minutes));
        }

        public MemoryCacheService(IOptions<MemoryCacheOptions> optionsAccessor, IMemoryCache memoryCache) : base(optionsAccessor)
        {
            _memoryCache = memoryCache;
        }
    }
}
