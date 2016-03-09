using Lanche.Core.Module;
using Lanche.MessageQueue.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.MessageQueue
{
    public class MessageQueueModule : Module
    {
        public override void PreInitialize()
        {
            IocManager.IocContainer.Install(new MqInstaller());
        }
    }
}
