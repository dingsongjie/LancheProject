using Lanche.MessageQueue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.Abstractions.MessageQueue
{
    public interface IMqConnectionSlover
    {
        ConnectionInfo GetConnectionInfo(string connectionName);
    }
}
