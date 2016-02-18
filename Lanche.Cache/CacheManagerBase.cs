using Lanche.Cache.Configuration;
using Lanche.Core.Dependency;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Immutable;

namespace Lanche.Cache
{
    /// <summary>
    /// cache manager 基类  
    /// </summary>
    public abstract class CacheManagerBase : ICacheManager
    {
        /// <summary>
        /// ioc manager
        /// </summary>
        protected readonly IIocManager IocManager;
        /// <summary>
        /// caching 配置
        /// </summary>
        protected readonly ICachingConfiguration Configuration;
        /// <summary>
        /// 缓存 dictionary 线程安全
        /// </summary>

        protected readonly ConcurrentDictionary<string, ICache> Caches;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="iocManager"></param>
        /// <param name="configuration"></param>
        protected CacheManagerBase(IIocManager iocManager, ICachingConfiguration configuration)
        {
            IocManager = iocManager;
            Configuration = configuration;
            Caches = new ConcurrentDictionary<string, ICache>();
        }
        /// <summary>
        /// 得到所有 cache对象
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<ICache> GetAllCaches()
        {
            return Caches.Values.ToImmutableList();
        }
        /// <summary>
        /// 根据 name 得到 cache
        /// </summary>
        /// <param name="name">name of cache</param>
        /// <returns></returns>
        public virtual ICache GetOrCreateCache(string name)
        {
            return Caches.GetOrAdd(name, (cacheName) =>
            {
                var cache = CreateCacheImplementation(cacheName);

                var configurators = Configuration.Configurators.Where(c => c.CacheName == null || c.CacheName == cacheName);//cache name == null 则 创建 全部

                foreach (var configurator in configurators)
                {
                    if (configurator.InitAction != null)
                    {
                        configurator.InitAction(cache);
                    }
                }

                return cache;
            });
        }
        /// <summary>
        /// dispose
        /// </summary>
        public virtual void Dispose()
        {
            foreach (var cache in Caches)
            {
                IocManager.Release(cache.Value);
            }

            Caches.Clear();
        }

        /// <summary>
        /// 创建缓存 实现方法
        /// </summary>
        /// <param name="name">缓存名称</param>
        /// <returns>缓存对象</returns>
        protected abstract ICache CreateCacheImplementation(string name);
    }
}
