using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.MongoDB.Extensions
{
    /// <summary>
    /// 成员信息扩展方法
    /// </summary>
    internal static class MemberInfoExtensions
    {
        /// <summary>
        /// 获得成员特性
        /// </summary>
        /// <typeparam name="T">特性类型</typeparam>
        /// <param name="memberInfo"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public static T GetSingleAttributeOrNull<T>(this MemberInfo memberInfo, bool inherit = true) where T : class
        {
            if (memberInfo == null)
            {
                throw new ArgumentNullException("memberInfo");
            }

            var attrs = memberInfo.GetCustomAttributes(typeof(T), inherit);
            if (attrs.Length > 0)
            {
                return (T)attrs[0];
            }

            return default(T);
        }
    }
}
