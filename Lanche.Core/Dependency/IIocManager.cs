using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Lanche.Core.Dependency
{
    /// <summary>
    /// This interface is used to directly perform dependency injection tasks.
    /// </summary>
    public interface IIocManager : IDisposable
    {
        /// <summary>
        /// windsor 容器
        /// </summary>
        IWindsorContainer IocContainer { get; }


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

        /// <summary>
        /// 获得实例
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        object Resolve(Type type);
        /// <summary>
        /// 获得实例
        /// </summary>
        /// <param name="type"></param>
        /// <param name="argumentsAsAnonymousType">构造参数</param>
        /// <returns></returns>
        object Resolve(Type type, object argumentsAsAnonymousType);

        /// <summary>
        /// 回收
        /// </summary>
        /// <param name="obj"></param>
        void Release(object obj);
        /// <summary>
        /// 是否已被注册
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        bool IsRegistered(Type type);
        /// <summary>
        /// 是否已被注册
        /// </summary>

        /// <returns></returns>
        bool IsRegistered<T>();




        /// <summary>
        /// 注册自己
        /// </summary>
        /// <typeparam name="T">Type </typeparam>
        /// <param name="lifeStyle">单例 还是 多实例</param>
        void Register<T>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where T : class;

        /// <summary>
        /// 注册一个Type
        /// </summary>
        /// <param name="type">Type </param>
        /// <param name="lifeStyle">单例 还是 多实例</param>
        void Register(Type type, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton);

        /// <summary>
        /// 注册Type 并提供 实现Type
        /// </summary>
        /// <typeparam name="TType">注册的 type</typeparam>
        /// <typeparam name="TImpl">实现的 type</typeparam>
        /// <param name="lifeStyle">生命周期</param>
        void Register<TType, TImpl>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where TType : class
            where TImpl : class, TType;

        /// <summary>
        /// 注册Type 并提供 实现Type
        /// </summary>
        /// <param name="type">注册的 type</param>
        /// <param name="impl">实现的 type</param>
        /// <param name="lifeStyle">生命周期</param>
        void Register(Type type, Type impl, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton);




        /// <summary>
        /// 如果未注册 则注册
        /// </summary>
        /// <typeparam name="TType">要注册的类型</typeparam>
        /// <param name="lifeStyle">类型的生存类型</param>
        void RegisterIfNot<TType>(DependencyLifeStyle lifeStyle) where TType : class;
        /// <summary>
        /// 替换原来的注册 与 autofac 不同 组建的注册 不会发改之前的注册 ，若要覆盖 调用此方法 替换原来的注册
        /// </summary>
        /// <typeparam name="TType">service</typeparam>
        /// <typeparam name="TImpl">implementation </typeparam>
        /// <param name="lifeStyle">生存周期</param>
        void Replace<TType, TImpl>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where TType : class
            where TImpl : class, TType;
        /// <summary>
        /// 替换原来的注册 与 autofac 不同 组建的注册 不会发改之前的注册 ，若要覆盖 调用此方法 替换原来的注册
        /// </summary>
        /// <param name="type">service</param>
        /// <param name="impl">implementation</param>
        /// <param name="lifeStyle">生存周期</param>
        void Replace(Type type, Type impl, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton);
    }
}
