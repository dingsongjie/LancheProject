using Castle.MicroKernel.Registration;
using Lanche.RabbitMq.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lanche.RabbitMq
{
    public class RabbitInstaller : IWindsorInstaller
    {
        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(Component.For<IRabbitMqConfiguration, DefaultRabbitMqConfiguration>().ImplementedBy<DefaultRabbitMqConfiguration>().LifestyleSingleton());
        }
    }
}
