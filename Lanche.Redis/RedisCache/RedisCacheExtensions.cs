using Lanche.Cache;
using Lanche.Core.Dependency;
using Lanche.Redis.RedisCache;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Owin
{
    public static class RedisCacheExtensions
    {
        public static IAppBuilder UseRedisCache(this IAppBuilder appBuilder,string connectionName )
        {
            IocManager.Instance.Replace(typeof(ICacheManager), typeof(DefaultRedisCacheManager));
            RedisCacheConst.RedisConnectionName = connectionName;
            return appBuilder;
        }
    }
}
