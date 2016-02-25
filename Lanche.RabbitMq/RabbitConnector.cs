using Lanche.Core.Dependency;
using Lanche.RabbitMq.Configuration;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.RabbitMq
{
    public class RabbitConnector : ISingleDependency
    {
        private readonly IRabbitMqConfiguration _rabbitMqConfiguration;
        public RabbitConnector(IRabbitMqConfiguration rabbitMqConfiguration)
        {
            _rabbitMqConfiguration=rabbitMqConfiguration;
        }

        public  IConnection Connect(RabbitConnectionInfo connectionInfo)
        {
            var connFactory = new ConnectionFactory()
            {
                HostName = connectionInfo.HostName,
                Port = connectionInfo.Port,
                UserName = connectionInfo.UserName,
                Password = connectionInfo.Password,
                VirtualHost = connectionInfo.VirtualHost,

                AutomaticRecoveryEnabled = _rabbitMqConfiguration.AutomaticRecovery,
                NetworkRecoveryInterval=_rabbitMqConfiguration.NetworkRecoveryInterval
            };

            var connection = connFactory.CreateConnection();
           

            return connection;
        }
    }
}
