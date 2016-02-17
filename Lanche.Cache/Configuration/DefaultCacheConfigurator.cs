using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.Cache.Configuration
{
    /// <summary>
    /// 默认缓存配置器
    /// </summary>
    internal class DefaultCacheConfigurator : ICacheConfigurator
    {
        /// <summary>
        /// 缓存名称
        /// </summary>
        public string CacheName { get; private set; }
        /// <summary>
        /// 初始化方法委托
        /// </summary>
        public Action<ICache> InitAction { get; private set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="initAction"></param>
        public DefaultCacheConfigurator(Action<ICache> initAction)
        {
            InitAction = initAction;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="cacheName"></param>
        /// <param name="initAction"></param>
        public DefaultCacheConfigurator(string cacheName, Action<ICache> initAction)
        {
            CacheName = cacheName;
            InitAction = initAction;
        }
    }
}
