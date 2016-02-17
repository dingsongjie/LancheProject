using Castle.MicroKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Lanche.Web.Mvc.Controller
{
    public class WindsorControllerFactory : DefaultControllerFactory
    {
         //iocmanger kernel
        private readonly IKernel _kernel; 

       /// <summary>
       /// 构造函数
       /// </summary>
       /// <param name="kernel"></param>
        public WindsorControllerFactory(IKernel kernel)
        {
            _kernel = kernel;
        }

       /// <summary>
       /// dispose
       /// </summary>
       /// <param name="controller"></param>
        public override void ReleaseController(IController controller)
        {
            _kernel.ReleaseComponent(controller);
        }

        
        /// <summary>
        /// create controller
        /// </summary>
        /// <param name="requestContext"></param>
        /// <param name="controllerType"></param>
        /// <returns></returns>
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                throw new HttpException(404, string.Format("The controller for path '{0}' could not be found.", requestContext.HttpContext.Request.Path));
            }

            return (IController)_kernel.Resolve(controllerType);
        }
    }
}
