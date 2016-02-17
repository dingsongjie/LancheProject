using Castle.DynamicProxy;
using Castle.MicroKernel.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Lanche.Core.Dependency
{
    public  class DefaultConventionalRegistrar
    {
        public static void Initialize(IIocManager manager,List<Assembly> assemblys)
        {
            assemblys.ForEach(m =>
                {//多实例
                    manager.IocContainer.Register(
                        Classes.FromAssembly(m)
                            .IncludeNonPublicTypes()
                            .BasedOn<ITransientDependency>()
                            .WithService.Self()
                            .WithService.DefaultInterfaces()
                            .LifestyleTransient()
                        );

                    //单例
                    manager.IocContainer.Register(
                        Classes.FromAssembly(m)
                            .IncludeNonPublicTypes()
                            .BasedOn<ISingleDependency>()
                            .WithService.Self()
                            .WithService.DefaultInterfaces()
                            .LifestyleSingleton()
                        );

                    //拦截器
                    manager.IocContainer.Register(
                        Classes.FromAssembly(m)
                            .IncludeNonPublicTypes()
                            .BasedOn<IInterceptor>()
                            .WithService.Self()
                            .LifestyleTransient()
                        );
                });
           
        }
    }
}
