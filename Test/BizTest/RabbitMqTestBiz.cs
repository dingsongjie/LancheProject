using Lanche.DynamicWebApi.Application;
using Lanche.MessageQueue;
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
        private readonly IMqChannel _mqCannel;
        private readonly IRabbitMqConfiguration _con;
        public RabbitMqTestBiz(IMqChannel mqCannel, IRabbitMqConfiguration con)
        {
            this._mqCannel = mqCannel;
            _con = con;
        }
        public void Send()
        {
            
                
          
        }
    }
}
