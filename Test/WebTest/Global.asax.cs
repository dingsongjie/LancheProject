using Castle.Facilities.Logging;
using Lanche.Core;
using Lanche.Core.Dependency;
using Lanche.Core.Reflection;
using Lanche.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebTest
{
    public class MvcApplication : WebHttpApplication
    {
        public override void Application_Start(object sender, EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
             //IocManager.Instance.IocContainer.AddFacility<LoggingFacility>(f => f.UseLog4Net().WithConfig("log4net.config"));   //log4 注入
            base.Application_Start(sender,e);
        }
    }
}
