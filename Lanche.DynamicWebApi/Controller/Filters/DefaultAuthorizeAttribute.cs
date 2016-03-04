using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Lanche.DynamicWebApi.Controller.Filters.Extensions;
using Lanche.Core.Thread;
using System.Net.Http;
using Lanche.Web.Exceptions;
using System.Net;
using Lanche.DynamicWebApi.Controller.Dynamic;

namespace Lanche.DynamicWebApi.Controller.Filters
{
    /// <summary>
    /// 最简单地实现 Todo:实现返回路径动态可配  
    /// </summary>
    public class DefaultAuthorizeAttribute : AuthorizeAttribute
       
    {
        ///// <summary>
        ///// 默认为 百度
        ///// </summary>
        //public string AuthenticationUrl { get; set; }
        /// <summary>
        /// 默认为当前request url
        /// </summary>
        public string ReturnUrl { get; set; }
        protected override void HandleUnauthorizedRequest(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (actionContext == null)
            {
                throw new ArgumentNullException ("actionContext");
            }
            //if (AuthenticationUrl == null)
            //{
            //    AuthenticationUrl = "http://www.baidu.com";
            //}
            //actionContext.Response = AsyncHelper.RunSync<HttpResponseMessage>(() => actionContext.ControllerContext.Request.GetRedirectResponse(AuthenticationUrl + "?ReturnUrl=" + actionContext.ControllerContext.Request.RequestUri));
           // throw new AuthorizationException();
            actionContext.Response = actionContext.Request.CreateResponse(
               HttpStatusCode.OK,
               new AjaxResponse(new ErrorInfo() { Message = "权限不足", UnAuthorizedRequest = true }) { }
               );
        }
    }
}
