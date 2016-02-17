
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http.Filters;

namespace Lanche.DynamicWebApi.Controller.Dynamic.Builders
{
    internal class ApiControllerBuilder<T> : IApiControllerBuilder<T> 
    {
        /// <summary>
        /// 服务前缀 加 controller 名称 
        /// </summary>
        private readonly string _serviceName;

       /// <summary>
       /// actionbuilder 集合
       /// </summary>
        private readonly IDictionary<string, ApiControllerActionBuilder<T>> _actionBuilders;

        /// <summary>
        /// controller 的 过滤器集合
        /// </summary>
        private IFilter[] _filters;
     

       /// <summary>
       /// 得到一个controllerbuilder实例
       /// </summary>
       /// <param name="serviceName"></param>
        public ApiControllerBuilder(string serviceName)
        {
            if (string.IsNullOrWhiteSpace(serviceName))
            {
                throw new ArgumentException("服务名不能为空", "serviceName");
            }

            if (!DynamicApiServiceNameHelper.IsValidServiceName(serviceName))
            {
                throw new ArgumentException("服务名不符合规范", "serviceName");
            }

            _serviceName = serviceName;

            _actionBuilders = new Dictionary<string, ApiControllerActionBuilder<T>>();
            foreach (var methodInfo in DynamicApiControllerActionHelper.GetMethodsOfType(typeof(T)))
            {
                _actionBuilders[methodInfo.Name] = new ApiControllerActionBuilder<T>(this, methodInfo);
            }
        }

        /// <summary>
        /// 添加过滤器
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        public IApiControllerBuilder<T> WithFilters(params IFilter[] filters)
        {
            _filters = filters;
            return this;
        }

      /// <summary>
      /// 返回一个指定name 的 actionbuilder
      /// </summary>
      /// <param name="methodName"></param>
      /// <returns></returns>
        public IApiControllerActionBuilder<T> ForMethod(string methodName)
        {
            if (!_actionBuilders.ContainsKey(methodName))
            {
                throw new Exception("在"+typeof(T).Name+"没有找到该方法："+methodName);
            }

            return _actionBuilders[methodName];
        }


        /// <summary>
        ///  build
        /// </summary>
        public void Build()
        {
            var controllerInfo = new DynamicApiControllerInfo(
                _serviceName,
                typeof(T),
                typeof(DynamicApiController<T>),           
                _filters
                );

            foreach (var actionBuilder in _actionBuilders.Values)
            {
                if (actionBuilder.DonotCreate)
                {
                    continue;
                }

                controllerInfo.Actions[actionBuilder.ActionName] = actionBuilder.BuildActionInfo();
            }

            DynamicApiControllerManager.Register(controllerInfo);
        }
    }
}
