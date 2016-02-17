using Lanche.DynamicWebApi.Controller.Dynamic.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace Lanche.DynamicWebApi.Controller.Dynamic.Selectors
{
    /// <summary>
    /// 自定义 controller selector 通过路径 解析得到 controller ，该selector 为 动态controller 接收请求入口 ，前面的动作 asp.net 帮我完成
    /// </summary>
    public class DynamicHttpControllerSelector : DefaultHttpControllerSelector
    {
        private readonly HttpConfiguration _configuration;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration">http 配置</param>
        public DynamicHttpControllerSelector(HttpConfiguration configuration)
            : base(configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// 根据请求 在 DynamicApiControllerManager 里的 controllerinfo字典 找到并 生成 HttpControllerDescriptor
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {

            if (request == null)
            {
                return base.SelectController(null);
            }
            //得到 routeData
            var routeData = request.GetRouteData();
            if (routeData == null)
            {
                return base.SelectController(request);
            }

            /// 得到 routedata 中的 {serviceNameWithAction}
            object serviceNameWithActionObj;

            if (!routeData.Values.TryGetValue("serviceNameWithAction", out serviceNameWithActionObj))
            {
                return base.SelectController(request);
            }
            string serviceNameWithAction = serviceNameWithActionObj.ToString();
            //去掉后缀
            if (serviceNameWithAction.EndsWith("/"))
            {
                serviceNameWithAction = serviceNameWithAction.Substring(0, serviceNameWithAction.Length - 1);
                routeData.Values["serviceNameWithAction"] = serviceNameWithAction;
            }

            //得到 controller
           
             //      1 ... pre/control/action  
            if (!DynamicApiServiceNameHelper.IsValidServiceNameWithAction(serviceNameWithAction))
            {
                return base.SelectController(request);
            }

            var serviceName = DynamicApiServiceNameHelper.GetServiceNameInServiceNameWithAction(serviceNameWithAction);
            var controllerInfo = DynamicApiControllerManager.FindOrNull(serviceName);
            if (controllerInfo == null)
            {
                return base.SelectController(request);
            }

        


            
            var controllerDescriptor = new DynamicHttpControllerDescriptor(_configuration, controllerInfo.ServiceName, controllerInfo.ApiControllerType, controllerInfo.Filters);
            controllerDescriptor.Properties["_DynamicApiControllerInfo"] = controllerInfo;
           
            return controllerDescriptor;
        }
    }
}
