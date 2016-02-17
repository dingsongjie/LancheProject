using Lanche.DynamicWebApi.Controller.Dynamic.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;

namespace Lanche.DynamicWebApi.Controller.Dynamic.Selectors
{
    /// <summary>
    /// controller action selector
    /// </summary>
    public class DynamicApiControllerActionSelector : ApiControllerActionSelector
    {
       /// <summary>
        /// 返回  HttpActionDescriptor
       /// </summary>
       /// <param name="controllerContext">controller 上下文</param>
       /// <returns></returns>
        public override HttpActionDescriptor SelectAction(HttpControllerContext controllerContext)
        {
            object controllerInfoObj;
            if (!controllerContext.ControllerDescriptor.Properties.TryGetValue("_DynamicApiControllerInfo", out controllerInfoObj))
            {
                return base.SelectAction(controllerContext);
            }

            
            var controllerInfo = controllerInfoObj as DynamicApiControllerInfo;
            if (controllerInfo == null)
            {
                throw new Exception(controllerInfoObj.GetType().Name + "并不是一个 DynamicApiControllerInfo, 初始化 ControllerDescriptor 有误 !!");
            }

           
            //得到 action
            var serviceNameWithAction = (controllerContext.RouteData.Values["serviceNameWithAction"] as string);
            if (serviceNameWithAction == null)
            {
                return base.SelectAction(controllerContext);
            }

            var actionName = DynamicApiServiceNameHelper.GetActionNameInServiceNameWithAction(serviceNameWithAction);

            return GetActionByActionName(
                controllerContext,
                controllerInfo,
                actionName
                );
        }
         /// <summary>
        /// 得到 HttpActionDescriptor
         /// </summary>
         /// <param name="controllerContext"></param>
         /// <param name="controllerInfo"></param>
         /// <param name="actionName"></param>
         /// <returns></returns>
        private static HttpActionDescriptor GetActionByActionName(HttpControllerContext controllerContext, DynamicApiControllerInfo controllerInfo, string actionName)
        {
           
            DynamicApiActionInfo actionInfo;
            if (!controllerInfo.Actions.TryGetValue(actionName, out actionInfo))
            {
                throw new Exception("请求路径有误,名称为" + actionName+" 的 action 并没有 在 该 controllerinfo 里的 actioninfo 集合中");
            }
            return new DynamicHttpActionDescriptor(controllerContext.ControllerDescriptor, actionInfo.Method, actionInfo.Filters);
        }
    }
}
