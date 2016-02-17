using Lanche.Core.Application;
using Lanche.Core.Module;
using Lanche.DynamicWebApi.Application;
using Lanche.DynamicWebApi.Controller.Dynamic.Builders;
using Lanche.RabbitMq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BizTest
{
    public class BizModule :Lanche.Core.Module. Module
    {
        public override void Initialize()
        {
            DynamicApiControllerBuilder.ForAll<ApplicationBizBase>(Assembly.GetExecutingAssembly(), "test").Build();
            RabbitConnectionInfo.Default = new RabbitConnectionInfo("localhost", "/", "guest", "guest", 5672);
            
            
        }
    }
}
