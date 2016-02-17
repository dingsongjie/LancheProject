using Lanche.Core.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Http;
using System.Web.Http.Filters;

namespace Lanche.DynamicWebApi.Controller.Dynamic.Builders
{
    /// <summary>
    /// controller action builder
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class ApiControllerActionBuilder<T> : IApiControllerActionBuilder<T> 
    {
       /// <summary>
       /// action 名称
       /// </summary>
        public string ActionName { get; private set; }

     

     /// <summary>
     /// control builder
     /// </summary>
        private readonly ApiControllerBuilder<T> _controllerBuilder;

        /// <summary>
        /// action methodinfo
        /// </summary>
        private readonly MethodInfo _methodInfo;

      /// <summary>
      /// 过滤器
      /// </summary>
        private IFilter[] _filters;

        /// <summary>
        /// 若为true 则不需要为此方法公开成服务
        /// </summary>
        public bool DonotCreate { get; private set; }

      /// <summary>
      /// 构造函数
      /// </summary>
      /// <param name="apiControllerBuilder">controllerbuilder</param>
      /// <param name="methodInfo">method info</param>
        public ApiControllerActionBuilder(ApiControllerBuilder<T> apiControllerBuilder, MethodInfo methodInfo)
        {
            _controllerBuilder = apiControllerBuilder;
            _methodInfo = methodInfo;
            ActionName = _methodInfo.Name;
        }

     

       
        public IApiControllerActionBuilder<T> ForMethod(string methodName)
        {
            return _controllerBuilder.ForMethod(methodName);
        }

        /// <summary>
        /// 为 action 添加过滤器
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        public IApiControllerActionBuilder<T> WithFilters(params IFilter[] filters)
        {
            _filters = filters;
            return this;
        }

      /// <summary>
      /// 不要将此action公开
      /// </summary>
      /// <returns></returns>
        public IApiControllerBuilder<T> DontCreateAction()
        {
            DonotCreate = true;
            return _controllerBuilder;
        }

      /// <summary>
      /// build
      /// </summary>
        public void Build()
        {
            _controllerBuilder.Build();
        }

       /// <summary>
       /// 生成此action 相关的 actioninfo
       /// </summary>
       /// <param name="conventionalVerbs"></param>
       /// <returns></returns>
        internal DynamicApiActionInfo BuildActionInfo(bool conventionalVerbs=true)
        {
            return new DynamicApiActionInfo(ActionName, _methodInfo, _filters);
        }

      

    }
}
