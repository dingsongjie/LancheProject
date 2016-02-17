using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.RabbitMq.Configuration
{
    public interface IRabbitMqConfiguration
    {
         Encoding BodyEncoding { get; set; }
        
    }
}
