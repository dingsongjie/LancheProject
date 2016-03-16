using Castle.Facilities.Logging;
using Lanche.Core.Dependency;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Owin
{
    public static class Log4NetExtensions
    {
        public static IAppBuilder UseLog4Net(this IAppBuilder appBuilder, string configPath)
        {
            IocManager.Instance.IocContainer.AddFacility<LoggingFacility>(f => f.UseLog4Net().WithConfig(configPath));   //log4 注入
            return appBuilder;
        }
    }
}
