using Lanche.MessageQueue.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.MessageQueue
{
    public class ConnectionInfoProvider : IConnectionInfoProvider
    {
        private IDictionary<string, ConnectionInfo> _currentConnectionInfos;
        public ConnectionInfoProvider()
        {
            _currentConnectionInfos = new Dictionary<string, ConnectionInfo>();
        }


        public ConnectionInfo GetConnectionInfo(string connectionName)
        {
            return _currentConnectionInfos[connectionName];
        }

        public void SetConnectionInfo(ConnectionInfo info)
        {
            _currentConnectionInfos[info.Name] = info;
        }
    }
}
