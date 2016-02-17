using Lanche.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.MemoryCache
{
    public class DefaultMemoryCache : CacheBase
    {
        private System.Runtime.Caching.MemoryCache _memoryCache;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">缓存唯一名称</param>
        public DefaultMemoryCache(string name)
            : base(name)
        {
            _memoryCache = new System.Runtime.Caching.MemoryCache(Name);
        }
        /// <summary>
        /// 根据 key查找 缓存的对象，如果没找到 返回null
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>缓存的对象</returns>
        public override object GetOrDefault(string key)
        {
            return _memoryCache.Get(key);
        }
        /// <summary>
        /// 缓存一个值（新加或覆盖）
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        /// <param name="slidingExpireTime">滑动过期时间</param>
        public override void Set(string key, object value, TimeSpan? slidingExpireTime = null)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            _memoryCache.Set(
                key,
                value,
                new CacheItemPolicy
                {
                    SlidingExpiration = slidingExpireTime ?? DefaultSlidingExpireTime
                });
        }
        /// <summary>
        /// 删除缓存 ,如果没找到不报错
        /// </summary>
        /// <param name="key">key</param>
        public override void Remove(string key)
        {
            _memoryCache.Remove(key);
        }
        /// <summary>
        /// 清空缓存
        /// </summary>
        public override void Clear()
        {
            _memoryCache.Dispose();
            _memoryCache = new System.Runtime.Caching.MemoryCache(Name);
        }
        /// <summary>
        /// 清空缓存 async
        /// </summary>
        public override void Dispose()
        {
            _memoryCache.Dispose();
            base.Dispose();
        }
    }
}
