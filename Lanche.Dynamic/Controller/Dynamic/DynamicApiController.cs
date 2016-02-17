using Lanche.Core.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Lanche.DynamicWebApi.Controller.Dynamic
{ 
    /// <summary>
    /// 动态 controller
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DynamicApiController<T> : ApiController, IDynamicApiController 
    {
        /// <summary>
        /// 内部 biz
        /// </summary>
        public T Biz { get; set; }
        public DynamicApiController()
        {
            Biz = IocManager.Instance.Resolve<T>();
        }

    }
}
