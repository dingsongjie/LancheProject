using Lanche.Core.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.MessageQueue.Abstractions
{
    public interface IConnectionInfoProvider : ISingleDependency
    {
        ConnectionInfo GetConnectionInfo(string connectionName);
        void SetConnectionInfo(ConnectionInfo info);

    }
}
