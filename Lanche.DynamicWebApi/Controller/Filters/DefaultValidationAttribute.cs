using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Net.Http;
using Lanche.Core.Dependency;
using Lanche.DynamicWebApi.Controller.Dynamic;

namespace Lanche.DynamicWebApi.Controller.Filters
{
    public class DefaultValidationAttribute : ActionFilterAttribute, ISingleDependency
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                ErrorInfo error = new ErrorInfo()
                {
                    IsModelValidateError = true,
                    ModelState = actionContext.ModelState
                };
                actionContext.Response = actionContext.Request.CreateResponse(
                    HttpStatusCode.OK,
                    new AjaxResponse(error));
               
                //context.Response.Content = new ObjectContent<AjaxResponse>(new AjaxResponse(_errorInfo), new JsonMediaTypeFormatter());
            }
        }
    }
}
