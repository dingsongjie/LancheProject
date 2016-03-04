using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.MessageQueue.Abstractions
{
   public interface IMessageQueryManager:IDisposable
    {
        IMqChannel GetChannal(string connectionName);
      
    }
}
