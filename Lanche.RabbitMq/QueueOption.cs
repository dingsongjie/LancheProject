using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.RabbitMq
{
    public class QueueOption
    {
        public string QueueName { get; set; }
        public bool Durable { get; set; }
        public bool Exclusive { get; set; }
        public bool AutoDelete { get; set; }
        public uint PrefetchSize { get; set; }
        public ushort PrefetchCount { get; set; }
        public bool Global { get; set; }
        public IDictionary<string, object> Arguments { get; set; }
        public static QueueOption Default = new QueueOption()
        {
            Durable = false,
            Exclusive = false,
            AutoDelete = false,
            PrefetchSize = 0,
            PrefetchCount = 1,
            Global = false


        };

    }
}
