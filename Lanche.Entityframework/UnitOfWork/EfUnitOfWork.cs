using Lanche.Core.Dependency;
using Lanche.Core.Reflection;
using Lanche.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Lanche.Entityframework.UnitOfWork
{
    /// <summary>
    /// ef 工作单元的实现
    /// </summary>
    public class EfUnitOfWork : UnitOfWorkBase
    {
        protected readonly IDictionary<Type, DbContext> ActiveDbContexts;

        protected IIocManager IocManager { get; private set; }
        
        protected TransactionScope CurrentTransaction;

        /// <summary>
        /// 构造函数
        /// </summary>
        public EfUnitOfWork(IIocManager iocManager, IUnitOfWorkDefaultOptions defaultOptions)
            : base(defaultOptions)
        {
            IocManager = iocManager;
            ActiveDbContexts = new Dictionary<Type, DbContext>();
        }

        protected override void BeginUow()
        {
            if (Options.IsTransactional == true)
            {
                var transactionOptions = new TransactionOptions
                {
                    IsolationLevel = Options.IsolationLevel.GetValueOrDefault(IsolationLevel.ReadUncommitted),
                };

                if (Options.Timeout.HasValue)
                {
                    transactionOptions.Timeout = Options.Timeout.Value;
                }

                CurrentTransaction = new TransactionScope(
                    Options.Scope.GetValueOrDefault(TransactionScopeOption.Required),
                    transactionOptions,
                    Options.AsyncFlowOption.GetValueOrDefault(TransactionScopeAsyncFlowOption.Enabled)
                    );
            }
        }

        public override void SaveChanges()
        {
            foreach (var activeDbContext in ActiveDbContexts.Values)
            {
                SaveChangesInDbContext(activeDbContext);
            }
           
        }

      

        protected override void CompleteUow()
        {
            SaveChanges();
            if (CurrentTransaction != null)
            {
                CurrentTransaction.Complete();
            }
        }

    


        public virtual TDbContext GetOrCreateDbContext<TDbContext>()
            where TDbContext : DbContext
        {
            DbContext dbContext;
            if (!ActiveDbContexts.TryGetValue(typeof(TDbContext), out dbContext))
            {
                dbContext = Resolve<TDbContext>();

              

                ActiveDbContexts[typeof(TDbContext)] = dbContext;
            }

            return (TDbContext)dbContext;
        }

        protected override void DisposeUow()
        {
          
            foreach (var dbContext in ActiveDbContexts.Values)
            {
                Release(dbContext);
            }
            

            if (CurrentTransaction != null)
            {
                CurrentTransaction.Dispose();
            }
        }

        protected virtual void SaveChangesInDbContext(DbContext dbContext)
        {
            dbContext.SaveChanges();
        }

      

        protected virtual TDbContext Resolve<TDbContext>()
        {
            return IocManager.Resolve<TDbContext>();
        }

        protected virtual void Release(DbContext dbContext)
        {
            dbContext.Dispose();
            IocManager.Release(dbContext);
        }

        protected override async Task CompleteUowAsync()
        {
            await SaveChangesAsync();
            if (CurrentTransaction != null)
            {
                CurrentTransaction.Complete();
            }
        }
        public override async Task SaveChangesAsync()
        {
            foreach (var dbContext in ActiveDbContexts.Values)
            {
                await SaveChangesInDbContextAsync(dbContext);
            }
        }
        protected virtual async Task SaveChangesInDbContextAsync(DbContext dbContext)
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
