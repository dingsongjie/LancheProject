using Lanche.Core.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.MessageQueue.WorkSpace
{
    public interface IMqChannelWorker : IMultipleDependency,IDisposable
    {
        bool IsDisposed { get; set; }
        string Id { get; }
    }
}
