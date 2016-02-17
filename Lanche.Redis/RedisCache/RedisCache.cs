using Lanche.Cache;
using Lanche.Redis.RedisCache.Provider;
using Lanche.Redis.RedisCache.Util;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lanche.Redis.RedisCache.Extensions;

namespace Lanche.Redis.RedisCache
{
    public class RedisCache : CacheBase
    {
        private readonly ConnectionMultiplexer _connectionMultiplexer;
        private readonly RedisCacheConst _cacheConst;

        public IDatabase Database
        {
            get
            {
                return _connectionMultiplexer.GetDatabase();
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public RedisCache(string name, IRedisConnectionProvider redisConnectionProvider, RedisCacheConst redisConst)
            : base(name)
        {
            _cacheConst = redisConst;
            var connectionString = redisConnectionProvider.GetConnectionString(_cacheConst.RedisConnectionName);
            _connectionMultiplexer = redisConnectionProvider.GetConnection(connectionString);
        }
        public override object GetOrDefault(string key)
        {
            var objbyte = Database.StringGet(GetLocalizedKey(key));
            return objbyte.HasValue
                ? SerializeUtil.Deserialize(objbyte)
                : null;
        }
        public async override Task<object> GetOrDefaultAsync(string key)
        {
            var objbyte = await Database.StringGetAsync(GetLocalizedKey(key));
            return objbyte.HasValue
                ? SerializeUtil.Deserialize(objbyte)
                : null;
        }

        public override void Set(string key, object value, TimeSpan? slidingExpireTime = null)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            Database.StringSet(
                GetLocalizedKey(key),
                SerializeUtil.Serialize(value),
                slidingExpireTime
                );
        }
        public async override Task SetAsync(string key, object value, TimeSpan? slidingExpireTime = null)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            await Database.StringSetAsync(
                 GetLocalizedKey(key),
                 SerializeUtil.Serialize(value),
                 slidingExpireTime
                 );

        }
        public override void Remove(string key)
        {
            Database.KeyDelete(GetLocalizedKey(key));
        }
        public async override Task RemoveAsync(string key)
        {
            await Database.KeyDeleteAsync(GetLocalizedKey(key));

        }

        public override void Clear()
        {
            Database.KeyDeleteWithPrefix(GetLocalizedKey("*"));
        }
        public async override Task ClearAsync()
        {
            await Database.KeyDeleteWithPrefixAsync(GetLocalizedKey("*"));

        }

        private string GetLocalizedKey(string key)
        {
            return "n:" + Name + ",c:" + key;
        }
    }
}
