using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using Lanche.Core.Reflection;

namespace Lanche.EntityFramework.Extensions
{
    /// <summary>
    /// dbcontext 扩展方法类
    /// </summary>
    internal static class DbContextExtensions
    {
        /// <summary>
        /// 得到dbcontext中所有实体类
        /// </summary>
        /// <param name="dbContextType"></param>
        /// <returns></returns>
        public static IEnumerable<Type> GetEntityTypes(this Type dbContextType)
        {
            return
                from property in dbContextType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                where
                    ReflectionHelper.IsAssignableToGenericType(property.PropertyType, typeof(IDbSet<>)) ||
                    ReflectionHelper.IsAssignableToGenericType(property.PropertyType, typeof(DbSet<>))
                select property.PropertyType.GetGenericArguments()[0];
        }
    }
}