using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.Cache.Configuration
{
    /// <summary>
    /// 缓存配置器
    /// </summary>
    public interface ICacheConfigurator
    {
        /// <summary>
        /// 配置的缓存名字
        /// </summary>
        string CacheName { get; }

        /// <summary>
        /// 创建缓存中调用的初始化操作.
        /// </summary>
        Action<ICache> InitAction { get; }
    }
}
