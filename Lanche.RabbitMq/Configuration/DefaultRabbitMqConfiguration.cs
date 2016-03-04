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
        /// <summary>
        /// 编码
        /// </summary>
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

        /// <summary>
        /// 是否自动回收
        /// </summary>
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
        /// <summary>
        /// 链接 自动回收间隔
        /// </summary>
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
