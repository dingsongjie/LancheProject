using Lanche.Core.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace Lanche.DynamicWebApi.Controller
{
   /// <summary>
    /// 自定义 HttpControllerActivator 根据 controllerDescriptor 和 controllerType 返回 controller
   /// </summary>
    public class DynamicApiControllerActivator : IHttpControllerActivator 
    {
        private readonly IIocManager _iocManager;

        public DynamicApiControllerActivator(IIocManager iocManager)
        {
            _iocManager = iocManager;
        }
        /// <summary>
        /// 返回 controller
        /// </summary>
        /// <param name="request"></param>
        /// <param name="controllerDescriptor"></param>
        /// <param name="controllerType"></param>
        /// <returns></returns>
        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            var controllerWrapper = new IDisposeWapper<IHttpController>(_iocManager, (IHttpController)_iocManager.Resolve(controllerType));
            request.RegisterForDispose(controllerWrapper);
            return controllerWrapper.Object;
        }
    }
}
