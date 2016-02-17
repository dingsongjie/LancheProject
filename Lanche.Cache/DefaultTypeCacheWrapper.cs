using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.Cache
{
    public class DefaultTypedCacheWrapper<TKey, TValue> : ITypedCache<TKey, TValue>
    {
        /// <summary>
        /// 唯一的 缓存名称
        /// </summary>
        public string Name
        {
            get { return InternalCache.Name; }
        }
        /// <summary>
        /// 默认滑动过期时间
        /// </summary>
        public TimeSpan DefaultSlidingExpireTime
        {
            get { return InternalCache.DefaultSlidingExpireTime; }
            set { InternalCache.DefaultSlidingExpireTime = value; }
        }
        /// <summary>
        /// 内部缓存对象
        /// </summary>
        public ICache InternalCache { get; private set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="internalCache"></param>
        public DefaultTypedCacheWrapper(ICache internalCache)
        {
            InternalCache = internalCache;
        }
        /// <summary>
        /// dispose
        /// </summary>
        public void Dispose()
        {
            InternalCache.Dispose();
        }
        /// <summary>
        /// 清空缓存
        /// </summary>
        public void Clear()
        {
            InternalCache.Clear();
        }
        /// <summary>
        /// 清空缓存 async
        /// </summary>
        public Task ClearAsync()
        {
            return InternalCache.ClearAsync();
        }
        /// <summary>
        /// 根据key 获取缓存的对象,如果没找到 则创建 并返回该对象
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="factory"> method of create cache</param>
        /// <returns>value</returns>
        public TValue GetOrCreate(TKey key, Func<TKey, TValue> factory)
        {
            return InternalCache.GetOrCreate(key, factory);
        }
        /// <summary>
        /// 根据key 获取缓存的对象,如果没找到 则创建 并返回该对象 async
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="factory"> method of create cache</param>
        /// <returns>value task</returns>
        public Task<TValue> GetOrCreateAsync(TKey key, Func<TKey, Task<TValue>> factory)
        {
            return InternalCache.GetOrCreateAsync(key, factory);
        }
        /// <summary>
        /// 根据 key查找 缓存的对象，如果没找到 返回null
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>value</returns>
        public TValue GetOrDefault(TKey key)
        {
            return InternalCache.GetOrDefault<TKey, TValue>(key);
        }
        /// <summary>
        /// 根据 key查找 缓存的对象，如果没找到 返回null async
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>value task</returns>
        public Task<TValue> GetOrDefaultAsync(TKey key)
        {
            return InternalCache.GetOrDefaultAsync<TKey, TValue>(key);
        }
        /// <summary>
        /// 缓存一个值（新加或覆盖）
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        /// <param name="slidingExpireTime">滑动过期时间</param>

        public void Set(TKey key, TValue value, TimeSpan? slidingExpireTime = null)
        {
            InternalCache.Set(key.ToString(), value, slidingExpireTime);
        }
        /// <summary>
        /// 缓存一个值（新加或覆盖） async
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        /// <param name="slidingExpireTime">滑动过期时间</param>
        /// <returns>task</returns>
        public Task SetAsync(TKey key, TValue value, TimeSpan? slidingExpireTime = null)
        {
            return InternalCache.SetAsync(key.ToString(), value, slidingExpireTime);
        }
        /// <summary>
        /// 删除缓存 ,如果没找到不报错
        /// </summary>
        /// <param name="key">key</param>
        public void Remove(TKey key)
        {
            InternalCache.Remove(key.ToString());
        }
        /// <summary>
        /// 删除缓存 ,如果没找到不报错 async
        /// </summary>
        /// <param name="key">key</param>
        public Task RemoveAsync(TKey key)
        {
            return InternalCache.RemoveAsync(key.ToString());
        }
    }
}
