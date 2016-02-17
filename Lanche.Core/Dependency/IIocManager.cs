using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lanche.Core.Dependency
{
    /// <summary>
    /// This interface is used to directly perform dependency injection tasks.
    /// </summary>
    public interface IIocManager : IIocRegistrar, IIocResolver, IDisposable
    {
        /// <summary>
        /// windsor 容器
        /// </summary>
        IWindsorContainer IocContainer { get; }

        /// <summary>
        /// 是否注册
        /// </summary>
        /// <param name="type">Type</param>
        new bool IsRegistered(Type type);

        /// <summary>
        /// 是否注册
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        new bool IsRegistered<T>();
    }
}
