using Lanche.Core.Module;
using Lanche.MessageQueue;
using Lanche.RabbitMq.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.RabbitMq
{
    [DependsOn(typeof(MessageQueueModule))]
   public  class RabbitMqModule : Module
    {
       public override void PreInitialize()
       {
         //  IocManager.IocContainer.Install(new RabbitInstaller());
           
       }
       public override void Initialize()
       {
           ConfigurationManager.Add<IRabbitMqConfiguration, DefaultRabbitMqConfiguration>();
       }
    }
}
