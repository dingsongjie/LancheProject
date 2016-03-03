using Lanche.Core;
using Lanche.Core.Reflection;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.Web
{
    public static class WebExtensions
    {
        public static IAppBuilder UseLancheProject(this IAppBuilder appBuilder)
        {
            CoreBootstrapper bootStrapper = new CoreBootstrapper();

            bootStrapper.IocManager.Register<IAssemblyFinder,WebAssemblyFinder>();
            bootStrapper.Initialize();
            return appBuilder;
           
        }
    }
}
