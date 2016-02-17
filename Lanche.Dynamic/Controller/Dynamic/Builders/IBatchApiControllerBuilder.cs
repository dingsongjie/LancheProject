using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http.Filters;

namespace Lanche.DynamicWebApi.Controller.Dynamic.Builders
{
    /// <summary>
    /// This interface is used to define a dynamic api controllers.
    /// </summary>
    /// <typeparam name="T">Type of the proxied object</typeparam>
    public interface IBatchApiControllerBuilder<T>
    {
        /// <summary>
        /// 需要过滤的 type
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IBatchApiControllerBuilder<T> Where(Func<Type, bool> predicate);

       /// <summary>
       /// controller 上的过滤器
       /// </summary>
       /// <param name="filters"></param>
       /// <returns></returns>
        IBatchApiControllerBuilder<T> WithFilters(params IFilter[] filters);

        /// <summary>
        /// 自定义服务名称
        /// </summary>
        /// <param name="serviceNameSelector"></param>
        /// <returns></returns>
        IBatchApiControllerBuilder<T> WithServiceName(Func<Type, string> serviceNameSelector);

      

      /// <summary>
      /// build
      /// </summary>
        void Build();
    }
}
