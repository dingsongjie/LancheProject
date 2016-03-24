using Lanche.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using EntityFramework.Extensions;
using System.Linq.Expressions;
using EntityFramework.BulkInsert.Extensions;
using System.Threading.Tasks;
using System.Reflection;
using Lanche.Domain.Repository.Paging;

namespace Lanche.Entityframework.UnitOfWork.Repository
{
   /// <summary>
   /// ef 仓库基类
   /// </summary>
   /// <typeparam name="TDbContext"></typeparam>
   /// <typeparam name="TEntity"></typeparam>
    public class EfRepositoryBase<TDbContext, TEntity> : RepositoryBase<TEntity>, IEfRepository<TEntity>
        where TEntity : class,new()
        where TDbContext : DbContext
    {
       
        protected virtual TDbContext Context { get { return _dbContextProvider.DbContext; } }

      
        protected virtual DbSet<TEntity> Table { get { return Context.Set<TEntity>(); } }

        private readonly IDbContextProvider<TDbContext> _dbContextProvider;

      
    
        public EfRepositoryBase(IDbContextProvider<TDbContext> dbContextProvider)
        {
            _dbContextProvider = dbContextProvider;
        }

        public override IQueryable<TEntity> GetAll()
        {
            return Table;
        }


        public override async Task<List<TEntity>> GetAllListAsync()
        {
            return await GetAll().ToListAsync();
        }

        public override async Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().Where(predicate).ToListAsync();
        }

        public override async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().SingleAsync(predicate);
        }

      

        public override async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().FirstOrDefaultAsync(predicate);
        }

       

        public override TEntity Insert(TEntity entity)
        {
            return Table.Add(entity);
        }


        public override Task<TEntity> InsertAsync(TEntity entity)
        {
            return Task.FromResult(Table.Add(entity));
        }
     


        public override TEntity Update(TEntity entity)
        {
            AttachIfNot(entity);
            Context.Entry(entity).State = EntityState.Modified;
            return entity;
        }
        public override Task<TEntity> UpdateAsync(TEntity entity)
        {
            AttachIfNot(entity);
            Context.Entry(entity).State = EntityState.Modified;
            return Task.FromResult(entity);
        }

        public override void Delete(TEntity entity)
        {
            AttachIfNot(entity);
            Table.Remove(entity);
            
        }
        public  override async Task<int> CountAsync()
        {
            return await GetAll().CountAsync();
        }
   
      
        protected virtual void AttachIfNot(TEntity entity)
        {
            if (!Table.Local.Contains(entity))
            {
                Table.Attach(entity);
            }
        }


        public virtual int Delete(System.Linq.Expressions.Expression<Func<TEntity, bool>> filter)
        {
            return GetAll().Where(filter).Delete();
        }

        public virtual int Update(System.Linq.Expressions.Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TEntity>> update)
        {
           
            return GetAll().Where(filter).Update( update);
        }


        public virtual void BulkInsert(IEnumerable<TEntity> entities, System.Data.SqlClient.SqlBulkCopyOptions options, int? bulkSize = null)
        {
           
           Context.BulkInsert( entities,options,bulkSize);
            
        }





        public virtual void BulkInsert(IEnumerable<TEntity> entities, int? bulkSize = null)
        {
            Context.BulkInsert(entities,  bulkSize);
        }


     

        public virtual System.Threading.Tasks.Task<int> DeleteAsync(Expression<Func<TEntity, bool>> filter)
        {
            return GetAll().Where(filter).DeleteAsync();
        }

        public virtual System.Threading.Tasks.Task<int> UpdateAsync(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TEntity>> update)
        {
            return GetAll().Where(filter).UpdateAsync(update);
        }
        public override async Task<Domain.Repository.Paging.PagingEntity<TEntity>> GetInPagingAsync(Expression<Func<TEntity, bool>> query, int pageIndex, int pageSize, string orderPropertyName, bool sort = true)
        {
            List<TEntity> entities;
            var count = await this.CountAsync(query);
            var skipNumber = (pageIndex - 1) * pageSize;
            var maxPage = (int)Math.Ceiling((double)count / pageSize);
            PropertyInfo orderPropertyInfo = typeof(TEntity).GetProperty(orderPropertyName);
            Type orderType = orderPropertyInfo.PropertyType;

            if (string.IsNullOrEmpty(orderPropertyName))
            {
                if (sort)
                {
                    entities = await this.GetAll().Where(query).Skip(skipNumber).Take(pageSize).ToListAsync();
                }
                else
                {
                    entities = await this.GetAll().Where(query).Skip(skipNumber).Take(pageSize).ToListAsync();
                }
            }
            else
            {
                var parameter = Expression.Parameter(typeof(TEntity), "m");
                var mExpr = Expression.Property(parameter, orderPropertyName);
                var orderByExp = Expression.Lambda(mExpr, parameter);

                var queryExpression = this.GetAll().Where(query);

                string methodName = sort ? "OrderBy" : "OrderByDescending";
                MethodCallExpression resultExp = Expression.Call(typeof(Queryable), methodName, new Type[] { typeof(TEntity), orderType }, queryExpression.Expression, Expression.Quote(orderByExp));
                queryExpression = queryExpression.Provider.CreateQuery<TEntity>(resultExp);

                entities = await queryExpression.Skip(skipNumber).Take(pageSize).ToListAsync();

            }

            var result = new PagingEntity<TEntity>()
            {
                Entities = entities,
                MaxPage = maxPage,
                PageIndex = pageIndex,
                PageSize = pageSize,
                EntityTotalCount = count
            };
            return result;
        }

        public Database GetDatebase()
        {
            return Context.Database;
        }


        public List<TEntity> GetAllListNoTracking()
        {
            return GetAll().AsNoTracking().ToList();
        }

        public Task<List<TEntity>> GetAllListNoTrackingAsync()
        {
            return GetAll().AsNoTracking().ToListAsync();
        }

        public List<TEntity> GetAllListNoTracking(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Where(predicate).AsNoTracking().ToList();
        }

        public Task<List<TEntity>> GetAllListNoTrackingAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Where(predicate).AsNoTracking().ToListAsync();
        }

        public TEntity SingleNoTracking(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().AsNoTracking().Single(predicate);
        }

        public Task<TEntity> SingleNoTrackingAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().AsNoTracking().SingleAsync(predicate);
        }

        public TEntity FirstOrDefaultNoTracking(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().AsNoTracking().FirstOrDefault(predicate);
        }

        public Task<TEntity> FirstOrDefaultNoTrackingAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        public PagingEntity<TEntity> GetInPagingNoTracking(Expression<Func<TEntity, bool>> query, int pageIndex, int pageSize, string orderPropertyName, bool sort = true)
        {
             List<TEntity> entities;
            var count = this.Count(query);
            var skipNumber = (pageIndex - 1) * pageSize;
            var maxPage = (int)Math.Ceiling((double)count / pageSize);
            PropertyInfo orderPropertyInfo = typeof(TEntity).GetProperty(orderPropertyName);
            Type orderType = orderPropertyInfo.PropertyType;

            if (string.IsNullOrEmpty(orderPropertyName))
            {
                if (sort)
                {
                    entities = this.GetAll().Where(query).Skip(skipNumber).Take(pageSize).AsNoTracking().ToList();
                }
                else
                {
                    entities = this.GetAll().Where(query).Skip(skipNumber).Take(pageSize).AsNoTracking().ToList();
                }
            }
            else
            {
                var parameter = Expression.Parameter(typeof(TEntity), "m");
                var mExpr = Expression.Property(parameter, orderPropertyName);
                var orderByExp = Expression.Lambda(mExpr, parameter);

                var queryExpression = this.GetAll().Where(query);

                string methodName = sort ? "OrderBy" : "OrderByDescending";
                MethodCallExpression resultExp = Expression.Call(typeof(Queryable), methodName, new Type[] { typeof(TEntity), orderType }, queryExpression.Expression, Expression.Quote(orderByExp));
                queryExpression = queryExpression.Provider.CreateQuery<TEntity>(resultExp);

                entities = queryExpression.Skip(skipNumber).Take(pageSize).AsNoTracking().ToList();

            }

            var result = new PagingEntity<TEntity>()
            {
                Entities = entities,
                MaxPage = maxPage,
                PageIndex = pageIndex,
                PageSize = pageSize,
                EntityTotalCount = count
            };
            return result;
        }
        

        public async Task<PagingEntity<TEntity>> GetInPagingNoTrackingAsync(Expression<Func<TEntity, bool>> query, int pageIndex, int pageSize, string orderPropertyName, bool sort = true)
        {
            List<TEntity> entities;
            var count = await this.CountAsync(query);
            var skipNumber = (pageIndex - 1) * pageSize;
            var maxPage = (int)Math.Ceiling((double)count / pageSize);
            PropertyInfo orderPropertyInfo = typeof(TEntity).GetProperty(orderPropertyName);
            Type orderType = orderPropertyInfo.PropertyType;

            if (string.IsNullOrEmpty(orderPropertyName))
            {
                if (sort)
                {
                    entities = await this.GetAll().Where(query).Skip(skipNumber).Take(pageSize).AsNoTracking().ToListAsync();
                }
                else
                {
                    entities = await this.GetAll().Where(query).Skip(skipNumber).Take(pageSize).AsNoTracking().ToListAsync();
                }
            }
            else
            {
                var parameter = Expression.Parameter(typeof(TEntity), "m");
                var mExpr = Expression.Property(parameter, orderPropertyName);
                var orderByExp = Expression.Lambda(mExpr, parameter);

                var queryExpression = this.GetAll().Where(query);

                string methodName = sort ? "OrderBy" : "OrderByDescending";
                MethodCallExpression resultExp = Expression.Call(typeof(Queryable), methodName, new Type[] { typeof(TEntity), orderType }, queryExpression.Expression, Expression.Quote(orderByExp));
                queryExpression = queryExpression.Provider.CreateQuery<TEntity>(resultExp);

                entities = await queryExpression.Skip(skipNumber).Take(pageSize).AsNoTracking().ToListAsync();

            }

            var result = new PagingEntity<TEntity>()
            {
                Entities = entities,
                MaxPage = maxPage,
                PageIndex = pageIndex,
                PageSize = pageSize,
                EntityTotalCount = count
            };
            return result;
        }
    }
}
