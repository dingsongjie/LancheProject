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
        /// <summary>
        /// 默认 redis 缓存 key
        /// </summary>
        private readonly RedisCacheConst _cacheConst;
        /// <summary>
        /// redis 操作对象
        /// </summary>
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
                ? Newtonsoft.Json.JsonConvert.DeserializeObject(SerializeUtil.Deserialize(objbyte).ToString())
                : null;
        }
        public async override Task<object> GetOrDefaultAsync(string key)
        {
            var objbyte = await Database.StringGetAsync(GetLocalizedKey(key));
            return objbyte.HasValue
                ? Newtonsoft.Json.JsonConvert.DeserializeObject(SerializeUtil.Deserialize(objbyte).ToString())
                : null;
        }
        /// <summary>
        /// 存储形式 为json格式
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="slidingExpireTime"></param>
        public override void Set(string key, object value, TimeSpan? slidingExpireTime = null)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(value);
            Database.StringSet(
                GetLocalizedKey(key),
                SerializeUtil.Serialize(json),
                slidingExpireTime
                );
        }
        public async override Task SetAsync(string key, object value, TimeSpan? slidingExpireTime = null)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(value);
            await Database.StringSetAsync(
                 GetLocalizedKey(key),
                 SerializeUtil.Serialize(json),
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
        /// <summary>
        /// dispose
        /// </summary>
        public override void Dispose()
        {
            _connectionMultiplexer.Dispose();
        }
        public override  TValue GetOrDefault<TValue>(string key)
        {
            var objbyte = Database.StringGet(GetLocalizedKey(key));
            return objbyte.HasValue
                ? Newtonsoft.Json.JsonConvert.DeserializeObject<TValue>(SerializeUtil.Deserialize(objbyte).ToString())
                : default(TValue);
        }
        public override async Task<TValue> GetOrDefaultAsync<TValue>(string key)
        {
            var objbyte = await Database.StringGetAsync(GetLocalizedKey(key));
            return objbyte.HasValue
                ? Newtonsoft.Json.JsonConvert.DeserializeObject<TValue>(SerializeUtil.Deserialize(objbyte).ToString())
                : default(TValue);
        }
        public override TValue GetOrDefault<TKey, TValue>(TKey key)
        {
            var objbyte = Database.StringGet(GetLocalizedKey(key.ToString()));
            return objbyte.HasValue
                ? Newtonsoft.Json.JsonConvert.DeserializeObject<TValue>(SerializeUtil.Deserialize(objbyte).ToString())
                : default(TValue);
        }
        public override async Task<TValue> GetOrDefaultAsync<TKey, TValue>(TKey key)
        {
            var objbyte = await Database.StringGetAsync(GetLocalizedKey(key.ToString()));
            return objbyte.HasValue
                ? Newtonsoft.Json.JsonConvert.DeserializeObject<TValue>(SerializeUtil.Deserialize(objbyte).ToString())
                : default(TValue);
        }
        
    }
}
