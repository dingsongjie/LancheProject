using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.RabbitMq
{
    public static class RabbitConnector
    {
        

        public static IConnection Connect(RabbitConnectionInfo connectionInfo)
        {
            var connFactory = new ConnectionFactory()
            {
                HostName = connectionInfo.HostName,
                Port = connectionInfo.Port,
                UserName = connectionInfo.UserName,
                Password = connectionInfo.Password,
                VirtualHost = connectionInfo.VirtualHost,
                
                AutomaticRecoveryEnabled = true
            };

            var connection = connFactory.CreateConnection();
           

            return connection;
        }
    }
}
