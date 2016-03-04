using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.MessageQueue
{
   public class ConnectionInfo
    {
       public string Name { get; private set; }
        public string HostName { get; private set; }
        public string VirtualHost { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public int Port { get; private set; }

        public ConnectionInfo(string name,string hostName, string virtualHost, string userName, string password, int port)
        {
            Name = name;
            HostName = hostName;
            VirtualHost = virtualHost;
            UserName = userName;
            Password = password;
            Port = port;
        }
    }
}
