using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http.Filters;

namespace Lanche.DynamicWebApi.Controller.Dynamic.Builders
{
  /// <summary>
  /// ApiController builder
  /// </summary>
  /// <typeparam name="T"></typeparam>
    public interface IApiControllerBuilder<T>
    {
        /// <summary>
        /// 添加 action 过滤器
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        IApiControllerBuilder<T> WithFilters(params IFilter[] filters);

      /// <summary>
      /// 得到ActionBuilder
      /// </summary>
      /// <param name="methodName">方法名</param>
      /// <returns></returns>
        IApiControllerActionBuilder<T> ForMethod(string methodName);

      

        /// <summary>
        /// build controller
        /// </summary>
        void Build();
    }
}
