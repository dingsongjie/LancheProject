using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.MessageQueue
{
   public class ConnectionInfo
    {
       public string Name { get;  set; }
        public string HostName { get;  set; }
        public string VirtualHost { get;  set; }
        public string UserName { get;  set; }
        public string Password { get;  set; }
        public int Port { get;  set; }

        public ConnectionInfo(string name,string hostName, string virtualHost, string userName, string password, int port)
        {
            Name = name;
            HostName = hostName;
            VirtualHost = virtualHost;
            UserName = userName;
            Password = password;
            Port = port;
        }
       public ConnectionInfo()
        {

        }
    }
}
