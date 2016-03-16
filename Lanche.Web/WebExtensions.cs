using Lanche.Core;
using Lanche.Core.Reflection;
using Lanche.Web;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Owin
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
