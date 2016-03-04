using Lanche.Core.Dependency;
using Lanche.MessageQueue;
using Lanche.MessageQueue.Abstractions;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.RabbitMq
{
   public static class RabbitMqExtensions
    {
       public static IAppBuilder UseRabbitMq(this IAppBuilder appBuilder)
       {
           IocManager.Instance.Replace<IMessageQueryManager, RabbitMqManager>(DependencyLifeStyle.Multiple);
           return appBuilder;
       }
       public static IAppBuilder UseMqConnection(this IAppBuilder appBuilder,ConnectionInfo connectionInfo)
       {
          var provider= IocManager.Instance.Resolve<IConnectionInfoProvider>();
          provider.SetConnectionInfo(connectionInfo);
          IocManager.Instance.Release(provider);
          return appBuilder;
       }
    }
}
