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
        public Car GetOne()
        {
            var cache = _memoryCacheManager.GetOrCreateCache("test1");
            return cache.GetOrDefault<string, Car>("Id3");
        }
        public void SetOneInRedis()
        {
            Car car = new Car() { Id = Guid.NewGuid(), Name = "111" };
            var cache = _redisCacheManager.GetOrCreateCache("test1");

            cache.SetAsync("Id", car);

            //var cache2 = _redisCacheManager.GetOrCreateCache("test2");
            //cache.SetAsync("Id", 123456);
            //var cache3= _redisCacheManager.GetOrCreateCache("test3");
            //cache.SetAsync("Id", 123456);
        }
        public async Task<Car> GetOneInRedis()
        {

            var cache = _redisCacheManager.GetOrCreateCache("test1");
            var value = await cache.GetOrCreateAsync<Car>("Id3", new Car() { Id = Guid.NewGuid(), Name = "222" });
            
              


            return value;
        }
    }
}
