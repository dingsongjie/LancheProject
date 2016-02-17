using Lanche.Core.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Http.Filters;

namespace Lanche.DynamicWebApi.Controller.Dynamic.Builders
{
    /// <summary>
    /// 批量动态生成 apicontroller
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class BatchApiControllerBuilder<T> : IBatchApiControllerBuilder<T> 
    {
        private readonly string _servicePrefix;
        private readonly Assembly _assembly;
        private IFilter[] _filters;
        private Func<Type, string> _serviceNameSelector;
        private Func<Type, bool> _typePredicate;


        public BatchApiControllerBuilder(Assembly assembly, string servicePrefix)
        {
            _assembly = assembly;
            _servicePrefix = servicePrefix;
        }
        /// <summary>
        /// biz type过滤作用 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IBatchApiControllerBuilder<T> Where(Func<Type, bool> predicate)
        {
            _typePredicate = predicate;
            return this;
        }
        /// <summary>
        /// 批量添加过滤器
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        public IBatchApiControllerBuilder<T> WithFilters(params IFilter[] filters)
        {
            _filters = filters;
            return this;
        }
        /// <summary>
        /// 自定义biz 公开出去的服务名（controller 名+ 前缀）
        /// </summary>
        /// <param name="serviceNameSelector"></param>
        /// <returns></returns>
        public IBatchApiControllerBuilder<T> WithServiceName(Func<Type, string> serviceNameSelector)
        {
            _serviceNameSelector = serviceNameSelector;
            return this;
        }

        /// <summary>
        /// 开始 build
        /// </summary>
        public void Build()
        {
            var types = _assembly.GetTypes().Where(m => m.IsPublic && typeof(T).IsAssignableFrom(m) && IocManager.Instance.IsRegistered(m));


            if (_typePredicate != null)
            {
                types = types.Where(t => _typePredicate(t));
            }

            foreach (var type in types)
            {
                var serviceName = _serviceNameSelector != null
                    ? _serviceNameSelector(type)
                    : GetDefaultServiceName(type);

                if (!string.IsNullOrWhiteSpace(_servicePrefix))
                {
                    serviceName = _servicePrefix + "/" + serviceName;
                }

                var builder = typeof(DynamicApiControllerBuilder)
                    .GetMethod("For", BindingFlags.Public | BindingFlags.Static)
                    .MakeGenericMethod(type)
                    .Invoke(null, new object[] { serviceName });

                if (_filters != null)
                {
                    builder.GetType()
                        .GetMethod("WithFilters", BindingFlags.Public | BindingFlags.Instance)
                        .Invoke(builder, new object[] { _filters });
                }



                builder.GetType()
                        .GetMethod("Build", BindingFlags.Public | BindingFlags.Instance)
                        .Invoke(builder, new object[0]);
            }
        }

        public static string GetDefaultServiceName(Type type)
        {
            var typeName = type.Name;

            if (typeName.EndsWith("ApplicationBiz"))
            {
                typeName = typeName.Substring(0, typeName.Length - "ApplicationBiz".Length);
            }
            else if (typeName.EndsWith("AppBiz"))
            {
                typeName = typeName.Substring(0, typeName.Length - "AppBiz".Length);
            }
            else if (typeName.EndsWith("Biz"))
            {
                typeName = typeName.Substring(0, typeName.Length - "Biz".Length);
            }
            else
            {
                throw new Exception("biz 的命名不符合规范");
            }

            return typeName;

        }
    }
}
