using Lanche.Domain.Repository.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.Domain.Repository
{
    /// <summary>
    /// 通用数据访问仓库
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public interface IRepository<TEntity> : IRepository where TEntity : class,new()
    {
        #region Select/Get/Query

     /// <summary>
     /// 得到 IQueryable ,以提供linq 查询能力
     /// </summary>
     /// <returns></returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// 返回所有实体List
        /// </summary>
        /// <returns>实体List</returns>
        List<TEntity> GetAllList();

        /// <summary>
        /// 返回所有实体List
        /// </summary>
        /// <returns>实体List</returns>
        Task<List<TEntity>> GetAllListAsync();

 

        /// <summary>
        /// 根据 lambda 返回 实体List
        /// </summary>
        /// <param name="predicate">where 条件</param>
        /// <returns>实体 list</returns>
        List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 根据 lambda 返回 实体List
        /// </summary>
        /// <param name="predicate">where 条件</param>
        /// <returns>实体 list</returns>
        Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate);

      

          /// <summary>
          /// 返回单个，找到多个 直接报错
          /// </summary>
          /// <param name="predicate">where 条件</param>
          /// <returns></returns>
        TEntity Single(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 返回单个，找到多个 直接报错
        /// </summary>
        /// <param name="predicate">where 条件</param>
        /// <returns></returns>
        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate);


        /// <summary>
        /// 得到第一个
        /// </summary>
        /// <param name="predicate">where 条件</param>
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 得到第一个
        /// </summary>
        /// <param name="predicate">where 条件</param>
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="query">where</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页size</param>
        /// <param name="orderPropertyName">order Property</param>
        /// <param name="sort"> 正或逆 </param>
        /// <returns>包含所有分页信息的数据传递对象</returns>
        PagingEntity<TEntity> GetInPaging(Expression<Func<TEntity, bool>> query, int pageIndex, int pageSize, string orderPropertyName, bool sort = true);

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="query">where</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页size</param>
        /// <param name="orderPropertyName">order Property</param>
        /// <param name="sort"> 正或逆 </param>
        /// <returns>包含所有分页信息的数据传递对象</returns>
        Task<PagingEntity<TEntity>> GetInPagingAsync(Expression<Func<TEntity, bool>> query, int pageIndex, int pageSize, string orderPropertyName, bool sort = true);

        #endregion

        #region Insert

      
        TEntity Insert(TEntity entity);


        Task<TEntity> InsertAsync(TEntity entity);





        #endregion

        #region Update

       /// <summary>
       /// 更新 单个 同步
       /// </summary>
       /// <param name="entity"></param>
       /// <returns></returns>
        TEntity Update(TEntity entity);

        /// <summary>
        /// 更新 单个实体 异步
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> UpdateAsync(TEntity entity);




        #endregion

        #region Delete

       /// <summary>
       /// 删除单个实体
       /// </summary>
       /// <param name="entity"></param>
        void Delete(TEntity entity);
        /// <summary>
        /// 删除单个实体 异步
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>

        Task DeleteAsync(TEntity entity);



        #endregion

        #region Aggregates

        
        int Count();

        Task<int> CountAsync();

        int Count(Expression<Func<TEntity, bool>> predicate);

        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 当个数超过 2^32-1
        /// </summary>
        /// <returns>Count of entities</returns>
        long LongCount();

        /// <summary>
        /// 当个数超过 2^32-1
        /// </summary>
        /// <returns>Count of entities</returns>
        Task<long> LongCountAsync();

        /// <summary>
        /// 当个数超过 2^32-1
        /// </summary>
        /// <returns>Count of entities</returns>
        long LongCount(Expression<Func<TEntity, bool>> predicate);

        Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate);
        #endregion
    }
}
