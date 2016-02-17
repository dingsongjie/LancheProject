using Lanche.Core.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.DynamicWebApi.Controller.Dynamic.Builders
{
    internal static class DynamicApiControllerActionHelper
    {
        /// <summary>
        /// 获得类中 符合的方法
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<MethodInfo> GetMethodsOfType(Type type)
        {
            var allMethods = new List<MethodInfo>();

            FillMethodsWithoutObjectMethods(type, BindingFlags.Public | BindingFlags.Instance, allMethods);

            return allMethods.Where(m => m.Name != "ExecuteAsync").ToList();
               
        }
        /// <summary>
        /// 判断方法是不是这个类的
        /// </summary>
        /// <param name="methodInfo">methodinfo</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static bool IsMethodOfType(MethodInfo methodInfo, Type type)
        {
            if (type.IsAssignableFrom(methodInfo.DeclaringType))
            {
                return true;
            }

            // 似乎有点问题?
            return false;
        }
        /// <summary>
        /// 把biz中的非object,IApplicationBiz继承得到的方法公开出来作为服务
        /// </summary>
        /// <param name="type">biz类型</param>
        /// <param name="flags">方法删选</param>
        /// <param name="members">list </param>
        private static void FillMethodsWithoutObjectMethods(Type type, BindingFlags flags, List<MethodInfo> members)
        {
            members.AddRange(type.GetMethods(flags).Where(m => m.DeclaringType != typeof(object) && m.DeclaringType != typeof(IApplicationBiz) && !IsPropertyAccessor(m)));

          
        }
        /// <summary>
        /// 排除 特殊方法（构造函数，属性 事件 包装的方法）
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        private static bool IsPropertyAccessor(MethodInfo method)
        {
            //MethodAttributes.HideBySig 的必要性 ？？

            return method.IsSpecialName && (method.Attributes & MethodAttributes.HideBySig) != 0;
        }
    }
}
