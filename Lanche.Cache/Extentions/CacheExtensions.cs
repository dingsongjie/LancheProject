using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.Cache
{
    /// <summary>
    /// ICache 扩展方法
    /// </summary>
    public static class CacheExtensions
    {
        /// <summary>
        /// 转换成 typed 的 cache
        /// </summary>
        /// <typeparam name="TKey">Key type</typeparam>
        /// <typeparam name="TValue">Value Type</typeparam>
        /// <param name="cache">缓存名称</param>
        /// <returns>default typed cache</returns>
        public static ITypedCache<TKey, TValue> AsTyped<TKey, TValue>(this ICache cache)
        {
            return new DefaultTypedCacheWrapper<TKey, TValue>(cache);
        }
        /// <summary>
        /// 根据key 获取缓存的对象,如果没找到 则创建 并返回该对象
        /// </summary>
        /// <typeparam name="TKey">key type</typeparam>
        /// <typeparam name="TValue">value type</typeparam>
        /// <param name="cache"> ICache</param>
        /// <param name="key">key </param>
        /// <param name="factory">create method</param>
        /// <returns>value</returns>
        public static TValue GetOrCreate<TKey, TValue>(this ICache cache, TKey key, Func<TKey, TValue> factory)
        {
            return (TValue)cache.GetOrCreate(key.ToString(), (k) => (object)factory(key));
        }
        /// <summary>
        /// 根据key 获取缓存的对象,如果没找到 则创建 并返回该对象
        /// </summary>
        /// <typeparam name="TValue">value type</typeparam>
        /// <param name="cache">cache</param>
        /// <param name="key">key string</param>
        /// <param name="factory">创建缓存的方法</param>
        /// <returns>value</returns>
        public static TValue GetOrCreate<TValue>(this ICache cache, string key, Func<string, TValue> factory)
        {
            return (TValue)cache.GetOrCreate(key, (k) => (object)factory(key));
        }

        /// <summary>
        /// 根据key 获取缓存的对象,如果没找到 则创建 并返回该对象 async
        /// </summary>
        /// <typeparam name="TKey">key type</typeparam>
        /// <typeparam name="TValue">value type</typeparam>
        /// <param name="cache"> ICache</param>
        /// <param name="key">key </param>
        /// <param name="factory">create method</param>
        /// <returns>value task</returns>
        public static async Task<TValue> GetOrCreateAsync<TKey, TValue>(this ICache cache, TKey key, Func<TKey, Task<TValue>> factory)
        {
            var value = await cache.GetOrCreateAsync(key.ToString(), async (keyAsString) =>
            {
                var v = await factory(key);
                return (object)v;
            });

            return (TValue)value;
        }
        /// <summary>
        /// 根据key 获取缓存的对象,如果没找到 则创建 并返回该对象 async
        /// </summary>
        /// <typeparam name="TValue">value type</typeparam>
        /// <param name="cache">cache</param>
        /// <param name="key">key string</param>
        /// <param name="factory">创建缓存的方法</param>
        /// <returns>value</returns>
        public static async Task<TValue> GetOrCreateAsync<TValue>(this ICache cache, string key, Func<string, Task<TValue>> factory)
        {
            var value = await cache.GetOrCreateAsync(key, async (keyAsString) =>
            {
                var v = await factory(key);
                return (object)v;
            });

            return (TValue)value;
        }

        ///// <summary>
        ///// 根据 key查找 缓存的对象，如果没找到 返回null
        ///// </summary>
        ///// <typeparam name="TKey">key type</typeparam>
        ///// <typeparam name="TValue">value type</typeparam>
        ///// <param name="cache">icache</param>
        ///// <param name="key"> key</param>
        ///// <returns>value</returns>

        //public static TValue GetOrDefault<TKey, TValue>(this ICache cache, TKey key)
        //{
        //    var value = cache.GetOrDefault(key.ToString());
        //    if (value == null)
        //    {
        //        return default(TValue);
        //    }

        //    return (TValue)value;
        //}
        ///// <summary>
        ///// 根据 key查找 缓存的对象，如果没找到 返回null
        ///// </summary>
        ///// <typeparam name="TValue">返回的类型</typeparam>
        ///// <param name="cache">cache</param>
        ///// <param name="key">key string</param>
        ///// <returns></returns>
        //public static TValue GetOrDefault<TValue>(this ICache cache, string key)
        //{
        //    var value = cache.GetOrDefault(key);
        //    if (value == null)
        //    {
        //        return default(TValue);
        //    }

        //    return (TValue)value;
        //}
        ///// <summary>
        ///// 根据 key查找 缓存的对象，如果没找到 返回null async
        ///// </summary>
        ///// <typeparam name="TKey">key type</typeparam>
        ///// <typeparam name="TValue">value type</typeparam>
        ///// <param name="cache">icache</param>
        ///// <param name="key"> key</param>
        ///// <returns>value task</returns>
        //public static async Task<TValue> GetOrDefaultAsync<TKey, TValue>(this ICache cache, TKey key)
        //{
        //    var value = await cache.GetOrDefaultAsync(key.ToString());
        //    if (value == null)
        //    {
        //        return default(TValue);
        //    }

        //    return (TValue)value;
        //}
        ///// <summary>
        /////  根据 key查找 缓存的对象，如果没找到 返回null async
        ///// </summary>
        ///// <typeparam name="TValue">返回类型</typeparam>
        ///// <param name="cache">cache</param>
        ///// <param name="key">key string类型</param>
        ///// <returns></returns>
        //public static async Task<TValue> GetOrDefaultAsync<TValue>(this ICache cache, string key)
        //{
        //    var value = await cache.GetOrDefaultAsync(key);
        //    if (value == null)
        //    {
        //        return default(TValue);
        //    }

        //    return (TValue)value;
        //}
    }
}
