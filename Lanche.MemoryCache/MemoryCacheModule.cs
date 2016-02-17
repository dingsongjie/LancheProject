using Lanche.Cache;
using Lanche.Core.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.MemoryCache
{
    [DependsOn(typeof(CacheModule))]
    public class MemoryCacheModule : Module
    {
        public override void PreInitialize()
        {
            base.PreInitialize();
        }
    }
}
