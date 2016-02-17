using Castle.MicroKernel.Registration;
using Lanche.Core.Module;
using Lanche.DynamicWebApi.Controller;
using Lanche.DynamicWebApi.Controller.Dynamic;
using Lanche.DynamicWebApi.Controller.Dynamic.Selectors;
using Lanche.DynamicWebApi.Controller.Filters;
using Lanche.Web;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace Lanche.DynamicWebApi
{
   
    [DependsOn(typeof(WebModule))]
    public class AbpWebApiModule : Module
    {
       
        public override void PreInitialize()
        {
            this.AllAssemblies = AssemblyFinder.GetAllAssemblies();
            base.PreInitialize();          
        }

       
        public override void Initialize()
        {
           // ApiControllerConventionalRegistrar.Initialize(this.IocManager, this.AllAssemblies);
            var httpConfiguration = GlobalConfiguration.Configuration;
            ///注入自定义 服务 替换 微软的
            InitializeAspNetServices(httpConfiguration);
            ///注册过滤器
            InitializeFilters(httpConfiguration);
            /// 注册 格式化
            InitializeFormatters(httpConfiguration);
            ///注册路由
            InitializeRoutes(httpConfiguration);
        }

        public override void PostInitialize()
        {
            foreach (var controllerInfo in DynamicApiControllerManager.GetAll())
            {
                IocManager.IocContainer.Register(
                    Component.For(controllerInfo.ApiControllerType)
                       // .Proxy.AdditionalInterfaces(controllerInfo.BizType) 
                      //  .Interceptors(controllerInfo.InterceptorType)
                        .LifestyleTransient()
                    );

             
            }
        }

        private void InitializeAspNetServices(HttpConfiguration httpConfiguration)
        {
            httpConfiguration.Services.Replace(typeof(IHttpControllerSelector), new DynamicHttpControllerSelector(httpConfiguration));
            httpConfiguration.Services.Replace(typeof(IHttpActionSelector), new DynamicApiControllerActionSelector());
            httpConfiguration.Services.Replace(typeof(IHttpControllerActivator), new DynamicApiControllerActivator(IocManager));
            //httpConfiguration.Services.Replace(typeof(IHttpActionInvoker), new DynamicApiControllerActionInvoker(IocManager));
        }

        private void InitializeFilters(HttpConfiguration httpConfiguration)
        {
            httpConfiguration.Filters.Add(IocManager.Resolve<DynamicControllerExceptionFilterAttribute>());
        }

        private static void InitializeFormatters(HttpConfiguration httpConfiguration)
        {
            httpConfiguration.Formatters.Remove(httpConfiguration.Formatters.JsonFormatter);
            httpConfiguration.Formatters.Remove(httpConfiguration.Formatters.XmlFormatter);
            var formatter = new JsonMediaTypeFormatter();
            formatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            httpConfiguration.Formatters.Add(formatter);
        
        }

        private static void InitializeRoutes(HttpConfiguration httpConfiguration)
        {
            //Dynamic Web APIs

            httpConfiguration.Routes.MapHttpRoute(
                name: "DynamicWebApi",
                routeTemplate: "api/services/{*serviceNameWithAction}"
                );

          
        }
    }
}
