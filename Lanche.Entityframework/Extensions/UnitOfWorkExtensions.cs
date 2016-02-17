using Lanche.Entityframework.UnitOfWork;
using Lanche.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Lanche.Entityframework.Extensions
{
    /// <summary>
    ///统一工作单元 扩展方法
    /// </summary>
    public static class UnitOfWorkExtensions
    {
       /// <summary>
       /// 得到当前工作单元的DbContext
       /// </summary>
       /// <typeparam name="TDbContext"></typeparam>
       /// <param name="unitOfWork"></param>
       /// <returns></returns>
        public static TDbContext GetDbContext<TDbContext>(this IUnitOfWork unitOfWork)
            where TDbContext : DbContext
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork");
            }

            if (!(unitOfWork is EfUnitOfWork))
            {
                throw new ArgumentException("unitOfWork is not type of " + typeof(EfUnitOfWork).FullName, "unitOfWork");
            }

            return (unitOfWork as EfUnitOfWork).GetOrCreateDbContext<TDbContext>();
        }
    }
}
