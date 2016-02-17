using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Lanche.DynamicWebApi.Controller.Filters.Extensions;
using Lanche.Core.Thread;
using System.Net.Http;

namespace Lanche.DynamicWebApi.Controller.Filters
{
    /// <summary>
    /// 最简单地实现 Todo:实现返回路径动态可配  
    /// </summary>
    public class DefaultAuthorizeAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (actionContext == null)
            {
                throw new ArgumentNullException ("actionContext");
            }

            actionContext.Response = AsyncHelper.RunSync <HttpResponseMessage>(()=> actionContext.ControllerContext.Request.GetRedirectResponse("http://www.baidu.com"+"?ReturnUrl="+actionContext.ControllerContext.Request.RequestUri));
        }
    }
}
