using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http.Filters;

namespace Lanche.DynamicWebApi.Controller.Dynamic.Builders
{
    /// <summary>
    /// ApiControllerActionBuilder 接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IApiControllerActionBuilder<T>
    {
       

        /// <summary>
        /// Used to specify another method definition.
        /// </summary>
        /// <param name="methodName">Name of the method in proxied type</param>
        /// <returns>Action builder</returns>
        IApiControllerActionBuilder<T> ForMethod(string methodName);

       /// <summary>
       /// 不要公开此action 为服务
       /// </summary>
       /// <returns></returns>
        IApiControllerBuilder<T> DontCreateAction();

        /// <summary>
        /// build start
        /// </summary>
        void Build();

        /// <summary>
        /// 添加过滤器
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        IApiControllerActionBuilder<T> WithFilters(params IFilter[] filters);
    }
}
