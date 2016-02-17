using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Lanche.Core.Dependency
{
    /// <summary>
    /// Ioc 功能执行者
    /// </summary>
    public class IocManager : IIocManager
    {
        /// <summary>
        /// 单例
        /// </summary>
        public static IocManager Instance { get; private set; }

        /// <summary>
        /// the Castle Windsor Container.
        /// </summary>
        public IWindsorContainer IocContainer { get; private set; }


        static IocManager()
        {
            Instance = new IocManager(); //等同 public static IocManager Instance=new IocManager();
        }

        public IocManager()
        {
            IocContainer = new WindsorContainer();


            //注册自己  保持单例
            IocContainer.Register(
                Component.For<IocManager, IIocManager>().LifestyleSingleton().UsingFactoryMethod(() => this)
                );
        }

        /// <summary>
        /// 注册 类型
        /// </summary>
        /// <typeparam name="TType">类型 type</typeparam>
        /// <param name="lifeStyle">生命周期</param>
        public void Register<TType>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton) where TType : class
        {
            IocContainer.Register(ApplyLifestyle(Component.For<TType>(), lifeStyle));
        }

        /// <summary>
        /// 注册类型
        /// </summary>
        /// <param name="type">类型 type</param>
        /// <param name="lifeStyle">生命周期</param>
        public void Register(Type type, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
        {
            IocContainer.Register(ApplyLifestyle(Component.For(type), lifeStyle));
        }

        /// <summary>
        /// 注册类型 并提供i的实现类型
        /// </summary>
        /// <typeparam name="TType">I</typeparam>
        /// <typeparam name="TImpl">C</typeparam>
        /// <param name="lifeStyle">生命周期</param>
        public void Register<TType, TImpl>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where TType : class
            where TImpl : class, TType
        {
            IocContainer.Register(ApplyLifestyle(Component.For<TType, TImpl>().ImplementedBy<TImpl>(), lifeStyle));
        }

        /// <summary>
        /// 注册类型并提供i,c
        /// </summary>
        /// <param name="type">i</param>
        /// <param name="impl">c</param>
        /// <param name="lifeStyle">生命周期</param>
        public void Register(Type type, Type impl, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
        {
            IocContainer.Register(ApplyLifestyle(Component.For(type, impl).ImplementedBy(impl), lifeStyle));
        }

        /// <summary>
        /// 是否已经注册
        /// </summary>
        /// <param name="type">类型 type</param>
        /// <returns></returns>
        public bool IsRegistered(Type type)
        {
            return IocContainer.Kernel.HasComponent(type);
        }

        /// <summary>
        /// 是否已经注册
        /// </summary>
        /// <typeparam name="TType">类型 type</typeparam>
        /// <returns></returns>
        public bool IsRegistered<TType>()
        {
            return IocContainer.Kernel.HasComponent(typeof(TType));
        }

        /// <summary>
        /// 得到 i 的实现类型实例
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <returns></returns>
        public T Resolve<T>()
        {
            return IocContainer.Resolve<T>();
        }

        /// <summary>
        /// 提供构造参数 得到 i 的实现类型实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="argumentsAsAnonymousType">params</param>
        /// <returns></returns>
        public T Resolve<T>(object argumentsAsAnonymousType)
        {
            return IocContainer.Resolve<T>(argumentsAsAnonymousType);
        }

        /// <summary>
        /// 提供构造参数 得到  实现类型实例
        /// </summary>
        /// <param name="type"></param>
        /// <returns>object类型 c</returns>
        public object Resolve(Type type)
        {
            return IocContainer.Resolve(type);
        }
        /// <summary>
        /// 提供构造参数，提供构造参数 得到 i 的实现类型实例
        /// </summary>
        /// <param name="type">i</param>
        /// <param name="argumentsAsAnonymousType">parameters</param>
        /// <returns>object类型 c</returns>
        public object Resolve(Type type, object argumentsAsAnonymousType)
        {
            return IocContainer.Resolve(type, argumentsAsAnonymousType);
        }

        /// <summary>
        /// 回收
        /// </summary>
        /// <param name="obj"></param>
        public void Release(object obj)
        {
            IocContainer.Release(obj);
        }

        /// <summary>
        /// dispose
        /// </summary>
        public void Dispose()
        {
            IocContainer.Dispose();
        }

        private static ComponentRegistration<T> ApplyLifestyle<T>(ComponentRegistration<T> registration, DependencyLifeStyle lifeStyle)
            where T : class
        {
            switch (lifeStyle)
            {
                case DependencyLifeStyle.Multiple:
                    return registration.LifestyleTransient();
                case DependencyLifeStyle.Singleton:
                    return registration.LifestyleSingleton();
                default:
                    return registration;
            }
        }
        /// <summary>
        /// 注册而整个程序集
        /// </summary>
        /// <param name="assembly"></param>
        public void RegisterAssemblyByConvention(Assembly assembly)
        {
            IocContainer.Install(FromAssembly.Instance(assembly));
        }
        /// <summary>
        /// 如果没有注册则 注册 ，否则 直接返回
        /// </summary>
        /// <typeparam name="TType"></typeparam>
        /// <param name="lifeStyle"></param>
        public void RegisterIfNot<TType>(DependencyLifeStyle lifeStyle) where TType : class
        {
            if (!IsRegistered<TType>())
            {
                this.Register<TType>(lifeStyle);
            }
        }
    }
}
