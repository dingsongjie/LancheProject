using Castle.MicroKernel.Registration;
using Lanche.Cache.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lanche.Cache
{
    /// <summary>
    /// 缓存 配置注册
    /// </summary>
    internal class CacheInstaller : IWindsorInstaller
    {
        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(Component.For<ICachingConfiguration, DefaultCachingConfiguration>().ImplementedBy<DefaultCachingConfiguration>().LifestyleSingleton());
        }
    }
}
