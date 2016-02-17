using Lanche.Cache;
using Lanche.DynamicWebApi.Application;
using Lanche.MemoryCache;
using Lanche.Redis.RedisCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizTest
{
    public class CacheTestBiz : ApplicationBizBase
    {
        private readonly DefaultMemoryCacheManager _memoryCacheManager;
        private readonly DefaultRedisCacheManager _redisCacheManager;
        public CacheTestBiz(DefaultMemoryCacheManager memoryCacheManager, DefaultRedisCacheManager redisCacheManager)
        {
            this._memoryCacheManager = memoryCacheManager;
            this._redisCacheManager = redisCacheManager;
        }
        public void SetOne()
        {
            var cache = _memoryCacheManager.GetOrCreateCache("test1");
            cache.Set("Id", 123456, new TimeSpan(1, 0, 0));
        }
        public object GetOne()
        {
            var cache = _memoryCacheManager.GetOrCreateCache("test1");
            return  cache.GetOrDefault<string,int>("Id");
        }
        public void SetOneInRedis()
        {
            var cache = _redisCacheManager.GetOrCreateCache("test1");
            cache.SetAsync("Id", 123456);
        }
        public async Task<int> GetOneInRedis()
        {
            var cache = _redisCacheManager.GetOrCreateCache("test1");
            var value = await cache.GetOrDefaultAsync<string, int>("Id");
            return value;
        }
    }
}
