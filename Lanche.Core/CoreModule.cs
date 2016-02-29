using Castle.DynamicProxy;
using Castle.MicroKernel.Registration;
using Lanche.Core.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.Core
{
    public class CoreModule : Lanche.Core.Module.Module
    {
        public override void PreInitialize()
        {
            this.AllAssemblies = AssemblyFinder.GetAllAssemblies();
            AddRegistrar();
            base.PreInitialize();
        }
        /// <summary>
        /// 注入
        /// </summary>
        private void AddRegistrar()
        {
            ///注册所有实现多例接口的
            DefaultConventionalRegistrar.AddRegistrant(m =>
                this.IocManager.IocContainer.Register(
                        Classes.FromAssembly(m)
                            .IncludeNonPublicTypes()
                            .BasedOn<ITransientDependency>()
                            .WithService.Self()
                            .WithService.DefaultInterfaces()
                            .LifestyleTransient()
                        ));
            ///注册所有实现单例接口的
            DefaultConventionalRegistrar.AddRegistrant(m =>
                this.IocManager.IocContainer.Register(
                        Classes.FromAssembly(m)
                            .IncludeNonPublicTypes()
                            .BasedOn<ISingleDependency>()
                            .WithService.Self()
                            .WithService.DefaultInterfaces()
                            .LifestyleSingleton()
                        ));
            ///注册所有实现拦截器接口的
            DefaultConventionalRegistrar.AddRegistrant(m =>
               this.IocManager.IocContainer.Register(
                       Classes.FromAssembly(m)
                           .IncludeNonPublicTypes()
                           .BasedOn<IInterceptor>()
                           .WithService.Self()
                           .LifestyleTransient()
                       ));
        }
        public override void Initialize()
        {

            DefaultConventionalRegistrar.Initialize(IocManager, this.AllAssemblies);
            base.Initialize();
        }
    }
}
