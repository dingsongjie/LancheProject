
using Lanche.Core.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Lanche.UnitOfWork
{
    /// <summary>
    /// uow 帮助类
    /// </summary>
    internal static class UnitOfWorkHelper
    {
       /// <summary>
       /// 是否 是 uow 需要切入的类
       /// </summary>
       /// <param name="type"></param>
       /// <returns></returns>
        public static bool IsConventionalUowClass(Type type)
        {
          //  return (typeof(IRepository).IsAssignableFrom(type) && !typeof(IOutOfUnitOfWork).IsAssignableFrom(type)) || typeof(IApplicationBiz).IsAssignableFrom(type);
            return typeof(IApplicationBiz).IsAssignableFrom(type);
        }

       /// <summary>
        /// 成员是否有 UnitOfWorkAttribute 标签
       /// </summary>
       /// <param name="methodInfo"></param>
       /// <returns></returns>
        public static bool HasUnitOfWorkAttribute(MemberInfo methodInfo)
        {
            return methodInfo.IsDefined(typeof(UnitOfWorkAttribute), true);
        }

       /// <summary>
        /// 获得 成员 UnitOfWorkAttribute 标签
       /// </summary>
       /// <param name="methodInfo"></param>
       /// <returns></returns>
        public static UnitOfWorkAttribute GetUnitOfWorkAttributeOrNull(MemberInfo methodInfo)
        {
            var attrs = methodInfo.GetCustomAttributes(typeof(UnitOfWorkAttribute), false);
            if (attrs.Length <= 0)
            {
                return null;
            }

            return (UnitOfWorkAttribute)attrs[0];
        }
    }
}
