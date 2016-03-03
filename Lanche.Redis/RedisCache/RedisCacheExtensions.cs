using Lanche.Cache;
using Lanche.Core.Dependency;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.Redis.RedisCache
{
    public static class RedisCacheExtensions
    {
        public static IAppBuilder UseRedisCache(this IAppBuilder appBuilder )
        {
            IocManager.Instance.Replace(typeof(ICacheManager), typeof(DefaultRedisCacheManager), DependencyLifeStyle.Multiple);
            return appBuilder;
        }
    }
}
