using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.Cache.Configuration
{
    /// <summary>
    /// 配置缓存
    /// </summary>
    public interface ICachingConfiguration
    {
        /// <summary>
        /// 所有的缓存配置器
        /// </summary>
        IReadOnlyList<ICacheConfigurator> Configurators { get; }

        /// <summary>
        /// 为缓存配置器进行初始化操作
        /// </summary>
        /// <param name="initAction"></param>
        void ConfigureAll(Action<ICache> initAction);

        /// <summary>
        /// 配置制定名称的缓存
        /// </summary>
        /// <param name="cacheName">缓存名称</param>
        /// <param name="initAction">缓存初始化操作</param>
        void Configure(string cacheName, Action<ICache> initAction);
    }
}
