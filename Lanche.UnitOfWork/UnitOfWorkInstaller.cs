using Castle.MicroKernel.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lanche.UnitOfWork
{
    /// <summary>
    /// uow 启动时 register
    /// </summary>
    internal class UnitOfWorkInstaller : IWindsorInstaller
    {
        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
             container.Register(Component.For<IUnitOfWorkDefaultOptions,UnitOfWorkDefaultOptions>().ImplementedBy<UnitOfWorkDefaultOptions>().LifestyleSingleton());
        }
    }
}
