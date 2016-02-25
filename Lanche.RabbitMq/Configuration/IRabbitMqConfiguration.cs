using Lanche.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.RabbitMq.Configuration
{
    public interface IRabbitMqConfiguration : IStartupConfiguration
    {
         Encoding BodyEncoding { get; set; }
         bool AutomaticRecovery { get; set; }
         TimeSpan NetworkRecoveryInterval { get; set; }
        
    }
}
