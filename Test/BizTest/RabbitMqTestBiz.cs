using Lanche.DynamicWebApi.Application;
using Lanche.MessageQueue;
using Lanche.MessageQueue.Abstractions;
using Lanche.RabbitMq;
using Lanche.RabbitMq.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BizTest
{
    public class RabbitMqTestBiz : ApplicationBizBase
    {
        private readonly IMessageQueryManager _manager;
       
        public RabbitMqTestBiz(IMessageQueryManager manager)
        {
            this._manager = manager;
            
        }
        public void Send()
        {
            var channel = _manager.GetChannal("test1");
            channel.Send("test", "hellow");
            //_manager.Dispose();
          
        }
    }
}
