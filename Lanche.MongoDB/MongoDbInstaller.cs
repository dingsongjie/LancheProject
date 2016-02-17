using Castle.MicroKernel.Registration;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.MongoDB
{
    public class MongoDbInstaller : IWindsorInstaller
    {
        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            //container.Register(Component.For<IMongoDbModuleConfiguration, DefaultMongoDbModuleConfiguration>().ImplementedBy<DefaultMongoDbModuleConfiguration>().LifestyleSingleton());
        }
    }
}
