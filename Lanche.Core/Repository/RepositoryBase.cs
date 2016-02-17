using Lanche.Core.Repository.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.Core.Repository
{
    /// <summary>
    /// 通用数据库访问仓库的基类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity>
        where TEntity : class,new()
    {
        public abstract IQueryable<TEntity> GetAll();

        public virtual List<TEntity> GetAllList()
        {
            return GetAll().ToList();
        }



        public virtual List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Where(predicate).ToList();
        }



        public virtual T Query<T>(Func<IQueryable<TEntity>, T> queryMethod)
        {
            return queryMethod(GetAll());
        }



        public virtual TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Single(predicate);
        }





        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().FirstOrDefault(predicate);
        }


        public abstract TEntity Insert(TEntity entity);



        public abstract TEntity Update(TEntity entity);



        public abstract void Delete(TEntity entity);




        public virtual int Count()
        {
            return GetAll().Count();
        }


        public virtual int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Where(predicate).Count();
        }


        public virtual long LongCount()
        {
            return GetAll().LongCount();
        }

        public virtual long LongCount(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Where(predicate).LongCount();
        }



        public virtual Paging.PagingEntity<TEntity> GetInPaging(Expression<Func<TEntity, bool>> query, int pageIndex, int pageSize, string orderPropertyName, bool sort = true)
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

                entities = queryExpression.Skip(skipNumber).Take(pageSize).ToList();

            }

            var result = new PagingEntity<TEntity>()
            {
                Entities = entities,
                MaxPage = maxPage,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            return result;
        }


        public virtual System.Threading.Tasks.Task<List<TEntity>> GetAllListAsync()
        {
            return Task.FromResult(GetAllList());
        }

        public virtual System.Threading.Tasks.Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(GetAllList(predicate));
        }

        public virtual System.Threading.Tasks.Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(Single(predicate));
        }

        public virtual System.Threading.Tasks.Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(FirstOrDefault(predicate));
        }

        public virtual System.Threading.Tasks.Task<PagingEntity<TEntity>> GetInPagingAsync(Expression<Func<TEntity, bool>> query, int pageIndex, int pageSize, string orderPropertyName, bool sort = true)
        {
            return Task.FromResult(GetInPaging(query, pageIndex, pageSize, orderPropertyName, sort));
        }

        public virtual System.Threading.Tasks.Task<TEntity> InsertAsync(TEntity entity)
        {
            return Task.FromResult(Insert(entity));
        }

        public virtual System.Threading.Tasks.Task<TEntity> UpdateAsync(TEntity entity)
        {
            return Task.FromResult(Update(entity));
        }

        public virtual System.Threading.Tasks.Task DeleteAsync(TEntity entity)
        {
            Delete(entity);
            return Task.FromResult(0);
        }



        public virtual System.Threading.Tasks.Task<int> CountAsync()
        {
            return Task.FromResult(Count());
        }

        public virtual System.Threading.Tasks.Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(Count(predicate));
        }

        public virtual System.Threading.Tasks.Task<long> LongCountAsync()
        {
            return Task.FromResult(LongCount());
        }

        public virtual System.Threading.Tasks.Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(LongCount(predicate));
        }
    }
}
