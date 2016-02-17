using Lanche.Cache;
using Lanche.Cache.Configuration;
using Lanche.Core.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.Redis.RedisCache
{
    public class DefaultRedisCacheManager : CacheManagerBase
    {
        public DefaultRedisCacheManager(IIocManager iocManager, ICachingConfiguration configuration)
            : base(iocManager, configuration)
        {
            IocManager.RegisterIfNot<RedisCache>(DependencyLifeStyle.Multiple);
        }
        protected override ICache CreateCacheImplementation(string name)
        {
            return IocManager.Resolve<RedisCache>(new { name });
        }
    }
}
