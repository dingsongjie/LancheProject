using Lanche.Core.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;

namespace Lanche.DynamicWebApi.Application
{
    public class ApplicationBizBase:IApplicationBiz,IHttpController
    {
        public Task<System.Net.Http.HttpResponseMessage> ExecuteAsync(HttpControllerContext controllerContext, System.Threading.CancellationToken cancellationToken)
        {
            throw new Exception("此方法不能被调用");
        }
    }
}
