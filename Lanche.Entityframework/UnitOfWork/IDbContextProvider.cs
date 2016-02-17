using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Lanche.Entityframework.UnitOfWork
{
    /// <summary>
    /// DbContext的包装
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    public interface IDbContextProvider<out TDbContext>
        where TDbContext : DbContext
    {
        TDbContext DbContext { get; } 
    }
}
