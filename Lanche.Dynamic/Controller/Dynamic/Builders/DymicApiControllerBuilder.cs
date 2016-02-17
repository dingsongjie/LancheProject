using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.DynamicWebApi.Controller.Dynamic.Builders
{
   /// <summary>
   /// 动态controller Builder
   /// </summary>
    public static class DynamicApiControllerBuilder
    {
      /// <summary>
      /// 注册单个Biz
      /// </summary>
      /// <typeparam name="T">Biz Type</typeparam>
      /// <param name="serviceName">服务名</param>
      /// <returns>ApiControllerBuilder</returns>
        public static IApiControllerBuilder<T> For<T>(string serviceName) 
        {
            return new ApiControllerBuilder<T>(serviceName);
        }

       /// <summary>
       /// 批量注册ApiController
       /// </summary>
       /// <typeparam name="T">Biz Type</typeparam>
       /// <param name="assembly">Biz所在程序集</param>
       /// <param name="servicePrefix">服务名前缀</param>
        /// <returns>BatchApiControllerBuilder</returns>
        public static IBatchApiControllerBuilder<T> ForAll<T>(Assembly assembly, string servicePrefix) 
        {
            return new BatchApiControllerBuilder<T>(assembly, servicePrefix);
        }
    }
}
