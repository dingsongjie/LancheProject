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
    /// Used to handle exceptions on web api controllers.
    /// </summary>
    public class DynamicControllerExceptionFilterAttribute : ExceptionFilterAttribute, ITransientDependency
    {
        private readonly ErrorInfo _errorInfo;
        public DynamicControllerExceptionFilterAttribute(ErrorInfo errorInfo)
        {
            _errorInfo = errorInfo;
        }
       /// <summary>
       /// 错误拦截器
       /// </summary>
       /// <param name="context">当前 action 上下文</param>
        public override void OnException(HttpActionExecutedContext context)
        {

            _errorInfo.Exception = context.Exception;
            _errorInfo.UnAuthorizedRequest = context.Exception is AuthorizationException;
            context.Response = context.Request.CreateResponse(
                HttpStatusCode.OK,
                new AjaxResponse(_errorInfo)
                );


        }
    }
}
