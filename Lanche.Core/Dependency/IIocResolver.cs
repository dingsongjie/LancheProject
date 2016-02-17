using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lanche.Core.Dependency
{
    /// <summary>
    /// 提供容器返回注册的实现的能力.
    /// </summary>
    public interface IIocResolver
    {
        /// <summary>
        /// 返回 实现 
        /// </summary> 
        /// <typeparam name="T">Type Register</typeparam>
        /// <returns>Type Register 实例</returns>
        T Resolve<T>();

        /// <summary>
        /// 返回 实现  并可以提供构造函数  
        /// </summary> 
        /// <typeparam name="T">要返回的type</typeparam>
        /// <param name="argumentsAsAnonymousType">构造函数参数</param>
        /// <returns>该类型实例</returns>
        T Resolve<T>(object argumentsAsAnonymousType);

       
        object Resolve(Type type);

        object Resolve(Type type, object argumentsAsAnonymousType);

     
        void Release(object obj);

        bool IsRegistered(Type type);

        bool IsRegistered<T>();

    }
}
