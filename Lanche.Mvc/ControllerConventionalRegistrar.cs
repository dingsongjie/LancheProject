using Castle.MicroKernel.Registration;
using Lanche.Core.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Lanche.Web.Mvc
{
    public class ControllerConventionalRegistrar
    {
        public static void Initialize(IIocManager manager, List<Assembly> assemblys)
        {
            assemblys.ForEach(m =>
            {  // controller
                manager.IocContainer.Register(
                    Classes.FromAssembly(m)
                        .IncludeNonPublicTypes()
                        .BasedOn<System.Web.Mvc.Controller>()
                        .WithService.Self()
                        .WithService.DefaultInterfaces()
                        .LifestyleTransient()
                    );

             
            });

        }
    }
}
