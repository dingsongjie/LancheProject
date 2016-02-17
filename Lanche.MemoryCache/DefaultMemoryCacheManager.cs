using Lanche.Cache;
using Lanche.Cache.Configuration;
using Lanche.Core.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Lanche.MemoryCache
{
    public class DefaultMemoryCacheManager : CacheManagerBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public DefaultMemoryCacheManager(IIocManager iocManager, ICachingConfiguration configuration)
            : base(iocManager, configuration)
        {
            IocManager.RegisterIfNot<DefaultMemoryCache>(DependencyLifeStyle.Multiple);
        }

        protected override ICache CreateCacheImplementation(string name)
        {
            return IocManager.Resolve<DefaultMemoryCache>(new { name });
        }
    }
}
