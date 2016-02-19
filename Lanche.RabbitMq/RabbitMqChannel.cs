using Lanche.MessageQueue;
using Lanche.RabbitMq.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.RabbitMq
{
    public class RabbitMqChannel : IMqChannel,Lanche.Core.Dependency.IMultipleDependency
    {
        private readonly IRabbitMqConfiguration _configuration;

        public IConnection Conncetion { get; set; }
        public RabbitMqChannel(IRabbitMqConfiguration configuration )
        {
            this.Conncetion = RabbitConnector.Connect(RabbitConnectionInfo.Default);
            this._configuration = configuration;
        }
        public Encoding MessageEncoding { get { return _configuration.BodyEncoding; } }
        public IModel Cannel { get{return Conncetion.CreateModel(); }  }
        public void Send(QueueOption queueOption, string message)
        {
            this.Cannel.QueueDeclare(queueOption.QueueName
                , queueOption.Durable
                , queueOption.Exclusive
                , queueOption.AutoDelete
                , queueOption.Arguments);
            var properties = this.Cannel.CreateBasicProperties();
            properties.DeliveryMode = 2;
            var body = MessageEncoding.GetBytes(message);
            this.Cannel.BasicPublish("", queueOption.QueueName, properties, body);
        }
            public void Send(string queue, string message)
        {
            QueueOption option = new QueueOption()
            {
                QueueName = queue
            };
            this.Cannel.QueueDeclare(option.QueueName
                , option.Durable
                , option.Exclusive
                , option.AutoDelete
                , option.Arguments);
            var properties = this.Cannel.CreateBasicProperties();
            properties.DeliveryMode = 2;
            var body = MessageEncoding.GetBytes(message);
            this.Cannel.BasicPublish("", option.QueueName, properties, body);
        }
        public void Dispose()
        {
            //Cannel.Dispose();
            Conncetion.Dispose();
        }


      
    }
}
