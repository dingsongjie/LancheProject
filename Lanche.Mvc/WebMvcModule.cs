using Lanche.Core.Module;
using Lanche.Web.Mvc.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Lanche.Web.Mvc
{
    [DependsOn(typeof(WebModule))]
    public class WebMvcModule : Module
    {
        public override void PreInitialize()
        {
            this.AllAssemblies = AssemblyFinder.GetAllAssemblies();
            base.PreInitialize();
        }
      

        /// <inheritdoc/>
        public override void Initialize()
        {

            ControllerConventionalRegistrar.Initialize(IocManager, this.AllAssemblies);
            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(IocManager.IocContainer.Kernel));
           
        }
    }
}
