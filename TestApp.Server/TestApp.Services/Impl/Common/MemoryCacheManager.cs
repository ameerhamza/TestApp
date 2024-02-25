using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Services.Contracts.Common;
using Microsoft.Extensions.Caching.Memory;


namespace TestApp.Services.Impl.Common
{
    public class MemoryCacheManager<T>: ICacheManager<T>
    {
        private readonly IMemoryCache _cache;

        public MemoryCacheManager()
        {
            _cache = new MemoryCache(new MemoryCacheOptions());
        }

        public void AddOrUpdate(string key, T value)
        {
            _cache.Set(key, value);
        }

        public bool TryGetValue(string key, out T value)
        {
            return _cache.TryGetValue(key, out value);
        }

        public T Get(string key)
        {
            _cache.TryGetValue(key, out var value);
            return (T)value;
        }

        public bool Remove(string key)
        {
            _cache.Remove(key);
            return true;
        }

    }
}
