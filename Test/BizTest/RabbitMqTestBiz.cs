using Lanche.DynamicWebApi.Application;
using Lanche.MessageQueue;
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
        public RabbitMqTestBiz(IMqChannel mqCannel)
        {
            this._mqCannel = mqCannel;
        }
        public void Send()
        {
            _mqCannel.Send("test", "hellow");
            _mqCannel.Dispose();
        }
    }
}
