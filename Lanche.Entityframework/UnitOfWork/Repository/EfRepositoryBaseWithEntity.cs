using Lanche.Core.Domain.Repository;
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
using Lanche.Core.Domain.Repository.Paging;

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

        public virtual System.Data.Entity.Infrastructure.DbRawSqlQuery<TResult> SqlQuery<TResult>(string sql, params object[] parameters)
        {
            return Context.Database.SqlQuery<TResult>(sql, parameters);
        }

        public int ExecuteSqlCommand(string sql, params object[] parameters)
        {
            return Context.Database.ExecuteSqlCommand(sql, parameters);
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


        public virtual System.Threading.Tasks.Task<int> ExecuteSqlCommandAsync(string sql, params object[] parameters)
        {
           return Context.Database.ExecuteSqlCommandAsync(sql, parameters);
        }

        public virtual System.Threading.Tasks.Task<int> DeleteAsync(Expression<Func<TEntity, bool>> filter)
        {
            return GetAll().Where(filter).DeleteAsync();
        }

        public virtual System.Threading.Tasks.Task<int> UpdateAsync(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TEntity>> update)
        {
            return GetAll().Where(filter).UpdateAsync(update);
        }
        public override async Task<Core.Domain.Repository.Paging.PagingDTO<TEntity>> GetInPagingAsync(Expression<Func<TEntity, bool>> query, int pageIndex, int pageSize, string orderPropertyName, bool sort = true)
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
                    entities = this.GetAll().Where(query).Skip(skipNumber).Take(pageSize).ToList();
                }
                else
                {
                    entities = this.GetAll().Where(query).Skip(skipNumber).Take(pageSize).ToList();
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

            var result = new PagingDTO<TEntity>()
            {
                Entities = entities,
                MaxPage = maxPage,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            return result;
        }
    }
}
