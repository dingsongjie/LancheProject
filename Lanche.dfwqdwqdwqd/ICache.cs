using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.Cache
{
    /// <summary>
    /// 缓存接口.
    /// </summary>
    public interface ICache : IDisposable
    {
        /// <summary>
        /// 唯一的 缓存名称
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 默认滑动过期时间
        /// </summary>
        TimeSpan DefaultSlidingExpireTime { get; set; }


        /// <summary>
        /// 根据key 获取缓存的对象,如果没找到 则创建 并返回该对象
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="factory">如果没找到，创建缓存的 method </param>
        /// <returns>缓存对象</returns>
        object GetOrCreate(string key, Func<string, object> factory);

        /// <summary>
        /// 根据key 获取缓存的对象 async,如果没找到 则创建 并返回该对象
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="factory">如果没找到，创建缓存的 method</param>
        /// <returns>缓存值的task</returns>
        Task<object> GetOrCreateAsync(string key, Func<string, Task<object>> factory);

        /// <summary>
        /// 根据 key查找 缓存的对象，如果没找到 返回null
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>缓存的对象</returns>
        object GetOrDefault(string key);

        /// <summary>
        /// 根据 key查找 缓存的对象，如果没找到 返回null async
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>缓存的对象的 task</returns>
        Task<object> GetOrDefaultAsync(string key);

        /// <summary>
        /// 缓存一个值（新加或覆盖）
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        /// <param name="slidingExpireTime">滑动过期时间</param>
        void Set(string key, object value, TimeSpan? slidingExpireTime = null);

        /// <summary>
        /// 缓存一个值（新加或覆盖） async
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        /// <param name="slidingExpireTime">滑动过期时间</param>
        /// <returns>task</returns>
        Task SetAsync(string key, object value, TimeSpan? slidingExpireTime = null);

        /// <summary>
        /// 删除缓存 ,如果没找到不报错
        /// </summary>
        /// <param name="key">key</param>
        void Remove(string key);

        /// <summary>
        /// 删除缓存 ,如果没找到不报错 async
        /// </summary>
        /// <param name="key">key</param>
        Task RemoveAsync(string key);

        /// <summary>
        /// 清空缓存
        /// </summary>
        void Clear();

        /// <summary>
        /// 清空缓存 async
        /// </summary>
        Task ClearAsync();
    }
}
