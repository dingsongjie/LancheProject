using Castle.MicroKernel.Registration;
using Lanche.Core.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Http;

namespace Lanche.DynamicWebApi
{
    //internal class ApiControllerConventionalRegistrar
    //{
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="manager"></param>
    //    /// <param name="assemblys"></param>
    //    public static void Initialize(IIocManager manager,List<Assembly> assemblys)
    //    {
    //        assemblys.ForEach(m =>
    //            { //注册所有 apiController
    //                manager.IocContainer.Register(
    //                    Classes.FromAssembly(m)
    //                        .IncludeNonPublicTypes()
    //                        .BasedOn<ApiController>()
    //                        .WithService.Self()
    //                     //   .WithService.DefaultInterfaces()
    //                        .LifestyleTransient()
    //                    );           dd
    //            });
           
    //    }
    //}
}
