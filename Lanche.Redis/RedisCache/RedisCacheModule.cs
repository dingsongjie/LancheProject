using Lanche.Cache;
using Lanche.Core.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.Redis.RedisCache
{
    [DependsOn(typeof(CacheModule))]
    public class RedisCacheModule : Module
    {
        public override void PreInitialize()
        {
            base.PreInitialize();
        }
    }
}
