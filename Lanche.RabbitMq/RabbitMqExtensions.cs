using Lanche.Abstractions.MessageQueue;
using Lanche.Core.Dependency;
using Lanche.MessageQueue;
using Lanche.MessageQueue.Abstractions;
using Lanche.RabbitMq;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Owin
{
   public static class RabbitMqExtensions
    {
       public static IAppBuilder UseRabbitMq(this IAppBuilder appBuilder)
       {
           IocManager.Instance.Replace<IMessageQueryManager, RabbitMqManager>(DependencyLifeStyle.Multiple);
           return appBuilder;
       }
       public static IAppBuilder UseMqConnection(this IAppBuilder appBuilder, string connectionName)
       {
          var provider= IocManager.Instance.Resolve<IConnectionInfoProvider>();
          var resolver = IocManager.Instance.Resolve<IMqConnectionSlover>();
          var info = resolver.GetConnectionInfo(connectionName);
          provider.SetConnectionInfo(info);
          IocManager.Instance.Release(provider);
          IocManager.Instance.Release(resolver);
          return appBuilder;
       }
    }
}
