using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace Lanche.DynamicWebApi.Controller.Dynamic
{
    /// <summary>
    /// 动态 apicontroller 信息 实际为 biz信息
    /// </summary>
    internal class DynamicApiControllerInfo
    {
        /// <summary>
        /// 共开出去的服务名
        /// </summary>
        public string ServiceName { get; private set; }

        /// <summary>
        /// biz type
        /// </summary>
        public Type BizType { get; private set; }

       /// <summary>
       /// controller type
       /// </summary>
        public Type ApiControllerType { get; private set; }


       /// <summary>
       /// 过滤器
       /// </summary>
        public IFilter[] Filters { get; set; }

        /// <summary>
        /// controller 种所有的 action
        /// </summary>
        public IDictionary<string, DynamicApiActionInfo> Actions { get; private set; }

        /// <summary>
        /// Creates a new <see cref="DynamicApiControllerInfo"/> instance.
        /// </summary>
        /// <param name="serviceName">Name of the service</param>
        /// <param name="bizType">biz </param>
        /// <param name="apiControllerType">Api Controller type</param>
  
        /// <param name="filters">过滤器</param>
        public DynamicApiControllerInfo(string serviceName, Type bizType, Type apiControllerType, IFilter[] filters = null)
        {
            ServiceName = serviceName;
            this.BizType = bizType;
            ApiControllerType = apiControllerType;
            Filters = filters ?? new IFilter[] { }; 
            Actions = new Dictionary<string, DynamicApiActionInfo>(StringComparer.InvariantCultureIgnoreCase);
        }
    }
}
