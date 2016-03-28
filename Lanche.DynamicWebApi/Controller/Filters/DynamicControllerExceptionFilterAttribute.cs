using Lanche.Core.Dependency;
using Lanche.DynamicWebApi.Controller.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using System.Net.Http;
using Lanche.Web.Exceptions;
using System.Net.Http.Formatting;

namespace Lanche.DynamicWebApi.Controller.Filters
{
    /// <summary>
    /// 拦截错误 返回自定义信息
    /// </summary>
    public class DynamicControllerExceptionFilterAttribute : ExceptionFilterAttribute,ISingleDependency
    {
      
     
       /// <summary>
       /// 错误拦截器
       /// </summary>
       /// <param name="context">当前 action 上下文</param>
        public override void OnException(HttpActionExecutedContext context)
        {
            ErrorInfo _errorInfo = new ErrorInfo();
            _errorInfo.Exception = context.Exception;        
           // context.Response = context.Request.CreateResponse(
            //    HttpStatusCode.OK,
            //    new AjaxResponse(_errorInfo) 
            //    );
            /// 在 拦截 ajax 错误时 上面写法 返回 500
           if(_errorInfo.Exception.InnerException!=null)      
            {
                //提示友好信息
                _errorInfo.Message = _errorInfo.Exception.InnerException.Message;
                context.Response = new HttpResponseMessage(HttpStatusCode.OK);
                context.Response.Content = new ObjectContent<AjaxResponse>(new AjaxResponse(_errorInfo), new JsonMediaTypeFormatter());
            }
            else
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.OK);
                context.Response.Content = new ObjectContent<AjaxResponse>(new AjaxResponse(_errorInfo), new JsonMediaTypeFormatter());
            }
           

        }
    }
}
