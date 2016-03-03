using Lanche.Cache;
using Lanche.DynamicWebApi.Application;
using Lanche.MemoryCache;
using Lanche.Redis.RedisCache;
using MongoDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BizTest
{
    public class CacheTestBiz : ApplicationBizBase
    {
        private readonly ICacheManager cacheManager;

        public CacheTestBiz(ICacheManager cacheManager)
        {
            this.cacheManager = cacheManager;
            
        }
        public void SetOne()
        {

            var cache = cacheManager.GetOrCreateCache("test1");
            cache.Set("Id", 123456, new TimeSpan(1, 0, 0));
        }
        public int GetOne()
        {
            var cache = cacheManager.GetOrCreateCache("test1");
            return cache.GetOrDefault<string, int>("Id");
        }
        public void SetOneInRedis()
        {
            Car car = new Car() { Id = Guid.NewGuid(), Name = "111" };
            var cache = cacheManager.GetOrCreateCache("test1");

            cache.SetAsync("Id", car);

            //var cache2 = _redisCacheManager.GetOrCreateCache("test2");
            //cache.SetAsync("Id", 123456);
            //var cache3= _redisCacheManager.GetOrCreateCache("test3");
            //cache.SetAsync("Id", 123456);
        }
        public async Task<Car> GetOneInRedis()
        {

            var cache = cacheManager.GetOrCreateCache("test1");
            var value = await cache.GetOrCreateAsync<Car>("Id", new Car() { Id = Guid.NewGuid(), Name = "222" });
            
              


            return value;
        }
    }
}
