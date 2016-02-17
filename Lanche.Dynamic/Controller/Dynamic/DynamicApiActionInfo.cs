using Lanche.DynamicWebApi.Controller.Dynamic.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace Lanche.DynamicWebApi.Controller.Dynamic
{
    /// <summary>
    ///  ActionInfo 实际为 biz action info
    /// </summary>
    internal class DynamicApiActionInfo
    {
        /// <summary>
        /// action name
        /// </summary>
        public string ActionName { get; private set; }

       /// <summary>
       /// methodinfo
       /// </summary>
        public MethodInfo Method { get; private set; }

        

       /// <summary>
       /// 过滤器
       /// </summary>
        public IFilter[] Filters { get; set; }

      /// <summary>
      /// 构造函数
      /// </summary>
      /// <param name="actionName">action 名称</param>
      /// <param name="method">method info</param>
      /// <param name="filters">过滤器</param>
        public DynamicApiActionInfo(string actionName, MethodInfo method, IFilter[] filters = null)
        {
            ActionName = actionName;
            Method = method;
            Filters = filters ?? new IFilter[] { }; 
        }
    }
}
