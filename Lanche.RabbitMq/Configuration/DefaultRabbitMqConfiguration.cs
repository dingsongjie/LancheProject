using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.RabbitMq.Configuration
{
    public class DefaultRabbitMqConfiguration : IRabbitMqConfiguration
    {
        public Encoding BodyEncoding
        {
            get
            {
                return Encoding.UTF8;
            }
            set
            {
                BodyEncoding = value;
            }
        }
    }
}
