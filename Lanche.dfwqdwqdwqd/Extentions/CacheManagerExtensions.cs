using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.Cache.Extentions
{ 
    /// <summary>
    /// cachemanager 扩展方法
    /// </summary>
    public static class CacheManagerExtensions
    {
        public static ITypedCache<TKey, TValue> GetCache<TKey, TValue>(this ICacheManager cacheManager, string name)
        {
            return cacheManager.GetOrCreateCache(name).AsTyped<TKey, TValue>();
        }
    }
}
