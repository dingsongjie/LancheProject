using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;


namespace Lanche.DynamicWebApi.Controller.Dynamic
{
   /// <summary>
   /// 包装了 动态controller 的主要方法
   /// </summary>
    internal static class DynamicApiControllerManager
    {
        /// <summary>
        /// DynamicApiControllerInfo 集合
        /// </summary>
        private static readonly Dictionary <string, DynamicApiControllerInfo> DynamicApiControllers;

        static DynamicApiControllerManager()
        {
            DynamicApiControllers = new Dictionary<string, DynamicApiControllerInfo>(StringComparer.InvariantCultureIgnoreCase);//忽略大小写
        }

       /// <summary>
        /// 将controllerinfo 添加到 DynamicApiControllerInfo集合中
       /// </summary>
        /// <param name="controllerInfo">DynamicApiControllerInfo</param>
        public static void Register(DynamicApiControllerInfo controllerInfo)
        {
            DynamicApiControllers[controllerInfo.ServiceName] = controllerInfo;
        }

        /// <summary>
        /// 返回 DynamicApiControllerInfo 集合中的 名字匹配的 DynamicApiControllerInfo ，没找到 返回 default
        /// 
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public static DynamicApiControllerInfo FindOrNull(string serviceName)
        {
            DynamicApiControllerInfo info;
            return DynamicApiControllers.TryGetValue(serviceName, out info) ? info : default(DynamicApiControllerInfo);
        }

        public static List<DynamicApiControllerInfo> GetAll()
        {
            return DynamicApiControllers.Values.ToList();
        }
    }
}
