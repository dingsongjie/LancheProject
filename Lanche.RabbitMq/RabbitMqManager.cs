using Lanche.MessageQueue.Abstractions;
using Lanche.RabbitMq.Configuration;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.RabbitMq
{
    public class RabbitMqManager : IMessageQueryManager
    {
        private readonly IRabbitMqConfiguration _configuration;
        private readonly IConnectionInfoProvider _connectionInfoProvider;
        private readonly RabbitConnector _rabbitConnector;
        public IConnection Conncetion { get;private set; }
        public Encoding MessageEncoding { get { return _configuration.BodyEncoding; } }
        public IModel Cannel { get { return Conncetion.CreateModel(); } }
        public RabbitMqManager(IRabbitMqConfiguration configuration, IConnectionInfoProvider provider, RabbitConnector connector)
        {

            this._connectionInfoProvider = provider;

            this._configuration = configuration;
            this._rabbitConnector = connector;
        }
        public MessageQueue.IMqChannel GetChannal(string connectionName)
        {
            var connection = _connectionInfoProvider.GetConnectionInfo(connectionName);
            this.Conncetion=_rabbitConnector.Connect(connection);
            var model = this.Conncetion.CreateModel();
            RabbitMqChannel channel = new RabbitMqChannel()
            {
                Configuration = this._configuration,
                Cannel = model

            };
            return channel;
        }

        public void Dispose()
        {
            /// connection dispose   channel 也 dispose 不需调用两次
            if(this.Conncetion==null)
            {
                return;
            }
            this.Conncetion.Dispose();
        }
    }
}
