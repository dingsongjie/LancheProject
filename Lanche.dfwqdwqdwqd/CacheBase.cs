using Nito.AsyncEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.Cache
{
    /// <summary>
    /// cache 基类
    /// </summary>
    public abstract class CacheBase : ICache
    {
        /// <summary>
        /// 缓存名称
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// 滑动过期时间
        /// </summary>

        public TimeSpan DefaultSlidingExpireTime { get; set; }
        /// <summary>
        /// 同步互斥锁
        /// </summary>

        protected readonly object SyncObj = new object();
        /// <summary>
        /// 异步互斥锁
        /// </summary>

        private readonly AsyncLock _asyncLock = new AsyncLock();

        /// <summary>
        /// 构造函数，默认滑动过期时间为 3600 秒
        /// </summary>
        /// <param name="name"></param>
        protected CacheBase(string name)
        {
            Name = name;
            DefaultSlidingExpireTime = TimeSpan.FromHours(1);
        }
        /// <summary>
        /// 根据key 获取缓存的对象,如果没找到 则创建 并返回该对象
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="factory">如果没找到，创建缓存的 method </param>
        /// <returns>缓存对象</returns>
        public virtual object GetOrCreate(string key, Func<string, object> factory)
        {
            var cacheKey = key;
            var item = GetOrDefault(key);
            if (item == null)
            {
                lock (SyncObj)
                {
                    item = GetOrDefault(key);
                    if (item == null)
                    {
                        item = factory(key);
                        if (item == null)
                        {
                            return null;
                        }

                        Set(cacheKey, item);
                    }
                }
            }

            return item;
        }
        /// <summary>
        /// 根据key 获取缓存的对象 async,如果没找到 则创建 并返回该对象k
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="factory">如果没找到，创建缓存的 method</param>
        /// <returns>缓存值的task</returns>
        public virtual async Task<object> GetOrCreateAsync(string key, Func<string, Task<object>> factory)
        {
            var cacheKey = key;
            var item = await GetOrDefaultAsync(key);
            if (item == null)
            {
                using (await _asyncLock.LockAsync())
                {
                    item = await GetOrDefaultAsync(key);
                    if (item == null)
                    {
                        item = await factory(key);
                        if (item == null)
                        {
                            return null;
                        }

                        await SetAsync(cacheKey, item);
                    }
                }
            }

            return item;
        }
        /// <summary>
        /// 根据 key查找 缓存的对象，如果没找到 返回null
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>缓存的对象</returns>
        public abstract object GetOrDefault(string key);
        /// <summary>
        /// 根据 key查找 缓存的对象，如果没找到 返回null async
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>缓存的对象的 task</returns>
        public virtual Task<object> GetOrDefaultAsync(string key)
        {
            return Task.FromResult(GetOrDefault(key));
        }
        /// <summary>
        /// 缓存一个值（新加或覆盖）
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        /// <param name="slidingExpireTime">滑动过期时间</param>
        public abstract void Set(string key, object value, TimeSpan? slidingExpireTime = null);
        /// <summary>
        /// 缓存一个值（新加或覆盖） async
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        /// <param name="slidingExpireTime">滑动过期时间</param>
        /// <returns>task</returns>
        public virtual Task SetAsync(string key, object value, TimeSpan? slidingExpireTime = null)
        {
            Set(key, value, slidingExpireTime);
            return Task.FromResult(0);
        }
        /// <summary>
        /// 删除缓存 ,如果没找到不报错
        /// </summary>
        /// <param name="key">key</param>
        public abstract void Remove(string key);
        /// <summary>
        /// 删除缓存 ,如果没找到不报错 async
        /// </summary>
        /// <param name="key">key</param>
        public virtual Task RemoveAsync(string key)
        {
            Remove(key);
            return Task.FromResult(0);
        }
        /// <summary>
        /// 清空缓存
        /// </summary>
        public abstract void Clear();
        /// <summary>
        /// 清空缓存 async
        /// </summary>
        public virtual Task ClearAsync()
        {
            Clear();
            return Task.FromResult(0);
        }

        public virtual void Dispose()
        {

        }
    }
}
