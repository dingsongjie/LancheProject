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

namespace Lanche.DynamicWebApi.Controller.Filters
{
    /// <summary>
    /// 拦截错误 返回自定义信息
    /// </summary>
    public class DynamicControllerExceptionFilterAttribute : ExceptionFilterAttribute,ISingleDependency
    {
      
        public DynamicControllerExceptionFilterAttribute()
        {
           
        }
       /// <summary>
       /// 错误拦截器
       /// </summary>
       /// <param name="context">当前 action 上下文</param>
        public override void OnException(HttpActionExecutedContext context)
        {
            ErrorInfo _errorInfo = new ErrorInfo();
            _errorInfo.Exception = context.Exception;        
            context.Response = context.Request.CreateResponse(
                HttpStatusCode.OK,
                new AjaxResponse(_errorInfo) 
                );


        }
    }
}
