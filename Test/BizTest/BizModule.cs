using Lanche.Core.Application;
using Lanche.Core.Module;
using Lanche.DynamicWebApi;
using Lanche.DynamicWebApi.Application;
using Lanche.DynamicWebApi.Controller.Dynamic.Builders;
using Lanche.Entityframework;
using Lanche.MessageQueue;
using Lanche.MongoDB;
using Lanche.RabbitMq;
using Lanche.Redis.RedisCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BizTest
{
    [DependsOn(typeof(AbpWebApiModule)
        , typeof(EntityframeworkMudule)
        , typeof(MessageQueueModule)
        , typeof(RedisCacheModule)
        , typeof(MongoDbModule))]
    public class BizModule :Lanche.Core.Module. Module
    {
        public override void Initialize()
        {
            DynamicApiControllerBuilder.ForAll<ApplicationBizBase>(Assembly.GetExecutingAssembly(), "test").Build();
          
            
            
        }
    }
}
