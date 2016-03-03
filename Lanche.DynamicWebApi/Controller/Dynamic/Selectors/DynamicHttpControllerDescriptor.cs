using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Lanche.DynamicWebApi.Controller.Dynamic.Selectors
{
   
    /// <summary>
    /// 自定义 ControllerDescriptor
    /// </summary>
    public class DynamicHttpControllerDescriptor : HttpControllerDescriptor
    { 
        /// <summary>
        /// 过滤器
        /// </summary>
        private readonly IFilter[] _filters;

       /// <summary>
       /// 构造函数
       /// </summary>
        /// <param name="configuration">HttpConfiguration</param>
        /// <param name="controllerName">controllerName</param>
        /// <param name="controllerType">controllerType</param>
        /// <param name="filters">filters</param>
        public DynamicHttpControllerDescriptor(HttpConfiguration configuration, string controllerName, Type controllerType, IFilter[] filters = null)
            : base(configuration, controllerName, controllerType)
        {
            _filters = filters;
        }

        /// <summary>
        /// 得到过滤器
        /// </summary>
        /// <returns></returns>
        public override Collection<IFilter> GetFilters()
        {
            var actionFilters = new Collection<IFilter>();

            if ((_filters.Count()>0))
            {
                foreach (var filter in _filters)
                {
                    actionFilters.Add(filter);
                }
            }

            foreach (var baseFilter in base.GetFilters())
            {
                actionFilters.Add(baseFilter);
            }

            return actionFilters;
        }
    }
}


