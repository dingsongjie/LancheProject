using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.Results;

namespace Lanche.DynamicWebApi.Controller.Dynamic.Selectors
{
    /// <summary>
    /// 自定义 HttpActionDescriptor
    /// </summary>
    public class DynamicHttpActionDescriptor : ReflectedHttpActionDescriptor
    {
        /// <summary>
        /// 过滤器
        /// </summary>
        private readonly IFilter[] _filters;

        /// <summary>
        /// 返回类型  默认 json
        /// </summary>
        public override Type ReturnType
        {
            get
            {
                return typeof(AjaxResponse);
            }
        }

        public DynamicHttpActionDescriptor(HttpControllerDescriptor controllerDescriptor, MethodInfo methodInfo, IFilter[] filters = null)
            : base(controllerDescriptor, methodInfo)
        {
            _filters = filters;
        }
        /// <summary>
        /// 通过表达式执行方法，具体在 ReflectedHttpActionDescriptor 中实现
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <param name="arguments"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override System.Threading.Tasks.Task<object> ExecuteAsync(HttpControllerContext controllerContext, System.Collections.Generic.IDictionary<string, object> arguments, System.Threading.CancellationToken cancellationToken)
        {

            dynamic Generic = controllerContext.Controller;
            var biz = Generic.Biz;
            controllerContext.Controller = controllerContext.Controller = biz;
            return base
                .ExecuteAsync(controllerContext, arguments, cancellationToken)
                .ContinueWith(task =>
                {
                    try
                    {
                        if (task.Result == null)
                        {

                            return new AjaxResponse();
                        }

                        if (task.Result is AjaxResponse)
                        {
                            return task.Result;
                        }

                        return new AjaxResponse(task.Result);
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }
                }, cancellationToken);
        }

        /// <summary>
        /// 得到过滤器
        /// </summary>
        /// <returns></returns>
        public override Collection<IFilter> GetFilters()
        {
            var actionFilters = new Collection<IFilter>();

            if (_filters.Count() > 0)
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
