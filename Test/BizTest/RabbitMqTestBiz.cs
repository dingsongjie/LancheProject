using Lanche.DynamicWebApi.Application;
using Lanche.MessageQueue;
using Lanche.RabbitMq;
using Lanche.RabbitMq.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            ///并发  测试   dispose
            for(int i=0;i<10000;i++)
            {
                IMqChannel c = new RabbitMqChannel(_con);
                c.Send("test", "hellow");
                c.Dispose();
            }
          
           // _mqCannel.Dispose();
        }
    }
}
