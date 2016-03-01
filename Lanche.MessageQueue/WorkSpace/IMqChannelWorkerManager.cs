using Lanche.Core.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.MessageQueue.WorkSpace
{
    public interface IMqChannelWorkerManager:IMultipleDependency,IDisposable
    {
        IMqChannelWorker Current { get;  }
        IMqChannelWorker GetWorker();
        
    }
}
