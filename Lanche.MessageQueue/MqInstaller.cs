using Castle.MicroKernel.Registration;
using Lanche.Abstractions.MessageQueue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.MessageQueue
{
    internal class MqInstaller : IWindsorInstaller
    {
        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(Component.For<IMqConnectionSlover, DefaultMqConnectionSlover>().ImplementedBy<DefaultMqConnectionSlover>().LifestyleSingleton()
               

                );
        }
    }
}
