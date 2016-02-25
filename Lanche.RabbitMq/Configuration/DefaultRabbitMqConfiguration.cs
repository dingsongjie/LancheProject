using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.RabbitMq.Configuration
{
    public class DefaultRabbitMqConfiguration : IRabbitMqConfiguration
    {
        private Encoding _bodyEncoding = Encoding.UTF8;
        private bool _automaticRecovery = true;
        private TimeSpan _networkRecoveryInterval = TimeSpan.FromSeconds(5);
        public Encoding BodyEncoding
        {
            get
            {
                return _bodyEncoding;
            }
            set
            {
                _bodyEncoding = value;
            }
        }


        public bool AutomaticRecovery
        {
            get
            {
                return _automaticRecovery;
            }
            set
            {
                _automaticRecovery = value;
            }
        }

        public TimeSpan NetworkRecoveryInterval
        {
            get
            {
                return _networkRecoveryInterval;
            }
            set
            {
                _networkRecoveryInterval = value;
            }
        }
    }
}
