using Castle.Core;
using Lanche.Core.Dependency;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Lanche.Entityframework.UnitOfWork
{
    /// <summary>
    /// 所有自定义DbContext的基类
    /// </summary>
    public class DbContextBase : DbContext, IInitializable, ITransientDependency
    {
        public void Initialize()
        {
            Database.Initialize(false);
        }
        public DbContextBase(string nameOrConnection)
            : base(nameOrConnection)
        {
          
        }
    }
}
