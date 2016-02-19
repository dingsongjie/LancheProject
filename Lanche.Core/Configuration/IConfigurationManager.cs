using Lanche.Core.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.Core.Configuration
{
    /// <summary>
    /// 配置操作对象
    /// </summary>
    public interface IConfigurationManager :  ISingleDependency
    {
        /// <summary>
        /// 获取相应的配置
        /// </summary>
        /// <typeparam name="TConfigType">配置类型</typeparam>
        /// <returns></returns>
        IStartupConfiguration Get<TConfigType>()
        where TConfigType : IStartupConfiguration;
        /// <summary>
        /// 获取相应的配置
        /// </summary>
        /// <param name="configurationType">配置类型</param>
        /// <returns></returns>
        IStartupConfiguration Get(Type configurationType);

      
        /// <summary>
        /// 替换原来的配置
        /// </summary>
        /// <typeparam name="TConfigType">配置类型</typeparam>
        /// <typeparam name="TConfigImpl">新的配置</typeparam>
        void Replace<TConfigType, TConfigImpl>()
            where TConfigType :class, IStartupConfiguration
            where TConfigImpl :class, TConfigType;
        /// <summary>
        /// 替换原来的配置
        /// </summary>
        /// <param name="configurationType">配置类型</param>
        /// <param name="configurationImpl">新的配置</param>
        void Replace(Type configurationType, Type configurationImpl);
        /// <summary>
        ///  添加配置
        /// </summary>
        /// <typeparam name="TIStartConfigu">配置类型</typeparam>
        /// <typeparam name="TIStartConfiguImpl">配置的实现</typeparam>
        void Add<TIStartConfigu, TIStartConfiguImpl>()
            where TIStartConfigu : class, IStartupConfiguration
            where TIStartConfiguImpl : class, TIStartConfigu;
        /// <summary>
        /// 添加配置
        /// </summary>
        /// <param name="configurationType">配置类型</param>
        /// <param name="configurationImpl">配置的实现</param>
        void Add(Type configurationType, Type configurationImpl);
    }
}
