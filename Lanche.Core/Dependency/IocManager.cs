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
            Instance = new IocManager();
        }
      
        public IocManager()
        {
            IocContainer = new WindsorContainer();
        

            //注册自己
            IocContainer.Register(
                Component.For<IocManager, IIocManager>().UsingFactoryMethod(() => this)
                );
        }
      
        
      
   


      
        public void Register<TType>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton) where TType : class
        {
            IocContainer.Register(ApplyLifestyle(Component.For<TType>(), lifeStyle));
        }

      
        public void Register(Type type, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
        {
            IocContainer.Register(ApplyLifestyle(Component.For(type), lifeStyle));
        }

    
        public void Register<TType, TImpl>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where TType : class
            where TImpl : class, TType
        {
            IocContainer.Register(ApplyLifestyle(Component.For<TType, TImpl>().ImplementedBy<TImpl>(), lifeStyle));
        }

     
        public void Register(Type type, Type impl, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
        {
            IocContainer.Register(ApplyLifestyle(Component.For(type, impl).ImplementedBy(impl), lifeStyle));
        }

 
        public bool IsRegistered(Type type)
        {
            return IocContainer.Kernel.HasComponent(type);
        }

      
        public bool IsRegistered<TType>()
        {
            return IocContainer.Kernel.HasComponent(typeof(TType));
        }

       
        public T Resolve<T>()
        {
            return IocContainer.Resolve<T>();
        }

     
        public T Resolve<T>(object argumentsAsAnonymousType)
        {
            return IocContainer.Resolve<T>(argumentsAsAnonymousType);
        }

    
        public object Resolve(Type type)
        {
            return IocContainer.Resolve(type);
        }

        public object Resolve(Type type, object argumentsAsAnonymousType)
        {
            return IocContainer.Resolve(type, argumentsAsAnonymousType);
        }

      
        public void Release(object obj)
        {
            IocContainer.Release(obj);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            IocContainer.Dispose();
        }

        private static ComponentRegistration<T> ApplyLifestyle<T>(ComponentRegistration<T> registration, DependencyLifeStyle lifeStyle)
            where T : class
        {
            switch (lifeStyle)
            {
                case DependencyLifeStyle.Transient:
                    return registration.LifestyleTransient();
                case DependencyLifeStyle.Singleton:
                    return registration.LifestyleSingleton();
                default:
                    return registration;
            }
        }

        public void RegisterAssemblyByConvention(Assembly assembly) 
        {
            IocContainer.Install(FromAssembly.Instance(assembly));
        }
        public void RegisterIfNot<TType>( DependencyLifeStyle lifeStyle) where TType:class
        {
            if(!IsRegistered<TType>())
            {
                this.Register<TType>(lifeStyle);
            }
        }
    }
}
