using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.MessageQueue
{
    public interface IMqChannel:IDisposable
    {
        void Send(string queue, string message);
       
    }
}
