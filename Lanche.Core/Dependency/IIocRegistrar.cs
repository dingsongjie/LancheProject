using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Lanche.Core.Dependency
{  /// <summary>
    /// 提供注册功能.
    /// </summary>
    //public interface IIocRegistrar
    //{
       

    //    /// <summary>
    //    /// 注册整个程序集
    //    /// </summary>
    //    /// <param name="assembly">Assembly </param>
    //    void RegisterAssemblyByConvention(Assembly assembly);

     
    //    /// <summary>
    //    /// 注册自己
    //    /// </summary>
    //    /// <typeparam name="T">Type </typeparam>
    //    /// <param name="lifeStyle">单例 还是 多实例</param>
    //    void Register<T>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
    //        where T : class;

    //    /// <summary>
    //    /// 注册一个Type
    //    /// </summary>
    //    /// <param name="type">Type </param>
    //    /// <param name="lifeStyle">单例 还是 多实例</param>
    //    void Register(Type type, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton);

    //    /// <summary>
    //    /// 注册Type 并提供 实现Type
    //    /// </summary>
    //    /// <typeparam name="TType">注册的 type</typeparam>
    //    /// <typeparam name="TImpl">实现的 type</typeparam>
    //    /// <param name="lifeStyle">生命周期</param>
    //    void Register<TType, TImpl>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
    //        where TType : class
    //        where TImpl : class, TType;

    //    /// <summary>
    //    /// 注册Type 并提供 实现Type
    //    /// </summary>
    //    /// <param name="type">注册的 type</param>
    //    /// <param name="impl">实现的 type</param>
    //    /// <param name="lifeStyle">生命周期</param>
    //    void Register(Type type, Type impl, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton);

    //    /// <summary>
    //    /// 是否注册
    //    /// </summary>
    //    /// <param name="type">Type </param>
    //    bool IsRegistered(Type type);

    //    /// <summary>
    //    /// 是否注册
    //    /// </summary>
    //    /// <typeparam name="TType">Type</typeparam>
    //    bool IsRegistered<TType>();
    //    /// <summary>
    //    /// 如果未注册 则注册
    //    /// </summary>
    //    /// <typeparam name="TType">要注册的类型</typeparam>
    //    /// <param name="lifeStyle">类型的生存类型</param>
    //    void RegisterIfNot<TType>(DependencyLifeStyle lifeStyle) where TType : class;
    //}
}
