using Castle.MicroKernel.Registration;
using Lanche.Core.Configuration;
using Lanche.Core.Module;
using Lanche.Core.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lanche.Core
{
    /// <summary>
    /// 为core 注册一些基本的Type
    /// </summary>
    internal class CoreInstaller : IWindsorInstaller
    {
        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(Component.For<ITypeFinder, BasicTypeFinder>().ImplementedBy<BasicTypeFinder>().LifestyleSingleton(),
                Component.For<IModuleFinder, DefaultModuleFinder>().ImplementedBy<DefaultModuleFinder>().LifestyleSingleton(),
                Component.For<IModuleManager, DefaultModuleManager>().ImplementedBy<DefaultModuleManager>().LifestyleSingleton(),
                 Component.For<IConfigurationManager, DefaultConfigurationManager>().ImplementedBy<DefaultConfigurationManager>().LifestyleSingleton()
               
                );
        }
    }
}
