using Lanche.Cache;
using Lanche.Core.Dependency;
using Lanche.MemoryCache;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Owin
{
    public static class MemoryCacheExtensions
    {
        public static IAppBuilder UseMemoryCache(this IAppBuilder appBuilder)
        {
            IocManager.Instance.Replace(typeof(ICacheManager), typeof(DefaultMemoryCacheManager));
            return appBuilder;
        }
    }
}
