using Lanche.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Lanche.Entityframework.Extensions;

namespace Lanche.Entityframework.UnitOfWork
{  
    /// <summary>
    /// 统一工作单元eF的实现
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    public class UnitOfWorkDbContextProvider<TDbContext> : IDbContextProvider<TDbContext>
        where TDbContext : DbContext
    {
        /// <summary>
        /// Gets the DbContext.
        /// </summary>
        public TDbContext DbContext { get { return _currentUnitOfWorkProvider.Current.GetDbContext<TDbContext>(); } }

        private readonly IUnitOfWorkProvider _currentUnitOfWorkProvider;

        /// <summary>
        /// Creates a new <see cref="UnitOfWorkDbContextProvider{TDbContext}"/>.
        /// </summary>
        /// <param name="currentUnitOfWorkProvider"></param>
        public UnitOfWorkDbContextProvider(IUnitOfWorkProvider currentUnitOfWorkProvider)
        {
            _currentUnitOfWorkProvider = currentUnitOfWorkProvider;
        }
    }
}
