using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Lanche.Domain.Repository;
using Lanche.Domain.Repository.Paging;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.Entityframework.UnitOfWork.Repository
{
    /// <summary>
    /// 实现 即拥有作为EfRepository的能力
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IEfRepository<TEntity> : IRepository<TEntity> where TEntity:class,new()
    {   
        /// <summary>
        /// 得到Database对向 以获得ado.net 访问数据库的部分能力
        /// </summary>
        /// <returns></returns>
        Database GetDatebase();
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        int Delete(Expression<Func<TEntity, bool>> filter);
        /// <summary>
        /// 批量删除 异步
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<int> DeleteAsync(Expression<Func<TEntity, bool>> filter);
        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        int Update(Expression<Func<TEntity, bool>> filter,Expression<Func<TEntity,TEntity>> update);
        /// <summary>
        /// 批量更新异步
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        Task<int> UpdateAsync(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TEntity>> update);
        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="options"></param>
        /// <param name="bulkSize"></param>
        void BulkInsert(IEnumerable<TEntity> entities, SqlBulkCopyOptions options, int? bulkSize=null);
        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="bulkSize"></param>

        void BulkInsert(IEnumerable<TEntity> entities, int? bulkSize=null);
        /// <summary>
        /// 返回所有实体List不再内存中缓存，不做状态跟踪
        /// </summary>
        /// <returns>实体List</returns>
        List<TEntity> GetAllListNoTracking();
        /// <summary>
        /// 返回所有实体List不再内存中缓存，不做状态跟踪，并根据select查询指定字段
        /// </summary>
        /// <returns>实体List</returns>
        List<TModel> GetAllListNoTracking<TModel>(Expression<Func<TEntity, TModel>> select) where TModel : class;
        /// <summary>
        /// 返回所有实体List不再内存中缓存，不做状态跟踪 异步方法
        /// </summary>
        /// <returns>实体List</returns>
        Task<List<TEntity>> GetAllListNoTrackingAsync();
        /// <summary>
        /// 返回所有实体List不再内存中缓存，不做状态跟踪 ，并根据select查询指定字段 异步方法 
        /// </summary>
        /// <returns>实体List</returns>
        Task<List<TModel>> GetAllListNoTrackingAsync<TModel>(Expression<Func<TEntity, TModel>> select)where TModel:class;
        /// <summary>
        /// 根据 lambda 返回 实体List 不做状态跟踪
        /// </summary>
        /// <param name="predicate">where 条件</param>
        /// <returns>实体 list</returns>
        List<TEntity> GetAllListNoTracking(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 根据 lambda 返回 实体List 不做状态跟踪，并根据select查询指定字段
        /// </summary>
        /// <param name="predicate">where 条件</param>
        /// <returns>实体 list</returns>
        List<TModel> GetAllListNoTracking<TModel>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TModel>> select) where TModel : class;
        /// <summary>
        /// 根据 lambda 返回 实体List 不做状态跟踪 异步
        /// </summary>
        /// <param name="predicate">where 条件</param>
        /// <returns>实体 list</returns>
        Task<List<TEntity>> GetAllListNoTrackingAsync(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 根据 lambda 返回 实体List 不做状态跟踪，并根据select查询指定字段 异步
        /// </summary>
        /// <param name="predicate">where 条件</param>
        /// <returns>实体 list</returns>
        Task<List<TModel>> GetAllListNoTrackingAsync<TModel>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TModel>> select) where TModel : class;

        /// <summary>
        /// 返回单个，找到多个 直接报错 不做状态跟踪
        /// </summary>
        /// <param name="predicate">where 条件</param>
        /// <returns></returns>
        TEntity SingleNoTracking(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 返回单个，找到多个 直接报错 不做状态跟踪，并根据select查询指定字段
        /// </summary>
        /// <param name="predicate">where 条件</param>
        /// <returns></returns>
        TModel SingleNoTracking<TModel>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TModel>> select) where TModel : class;
        /// <summary>
        /// 返回单个，找到多个 直接报错 不做状态跟踪 async
        /// </summary>
        /// <param name="predicate">where 条件</param>
        /// <returns></returns>
        Task<TEntity> SingleNoTrackingAsync(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 返回单个，找到多个 直接报错 不做状态跟踪，并根据select查询指定字段 async
        /// </summary>
        /// <param name="predicate">where 条件</param>
        /// <returns></returns>
        Task<TModel> SingleNoTrackingAsync<TModel>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TModel>> select) where TModel : class;
        /// <summary>
        /// 得到第一个 或者 null  不做状态跟踪
        /// </summary>
        /// <param name="predicate">where 条件</param>
        TEntity FirstOrDefaultNoTracking(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 得到第一个 或者 null  不做状态跟踪，并根据select查询指定字段
        /// </summary>
        /// <param name="predicate">where 条件</param>
        TModel FirstOrDefaultNoTracking<TModel>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TModel>> select) where TModel : class;
        /// <summary>
        /// 得到第一个 或者 null   不做状态跟踪 async
        /// </summary>
        /// <param name="predicate">where 条件</param>
        Task<TEntity> FirstOrDefaultNoTrackingAsync(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 得到第一个 或者 null   不做状态跟踪，并根据select查询指定字段 async
        /// </summary>
        /// <param name="predicate">where 条件</param>
        Task<TModel> FirstOrDefaultNoTrackingAsync<TModel>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TModel>> select) where TModel : class;
        /// <summary>
        /// 分页 不做状态跟踪
        /// </summary>
        /// <param name="query">where</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页size</param>
        /// <param name="orderPropertyName">order Property</param>
        /// <param name="sort"> 正或逆 </param>
        /// <returns>包含所有分页信息的数据传递对象</returns>
        PagingEntity<TEntity> GetInPagingNoTracking(Expression<Func<TEntity, bool>> query, int pageIndex, int pageSize, string orderPropertyName, bool sort = true);
        /// <summary>
        /// 分页 不做状态跟踪，并根据select查询指定字段
        /// </summary>
        /// <param name="query">where</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页size</param>
        /// <param name="orderPropertyName">order Property</param>
        /// <param name="sort"> 正或逆 </param>
        /// <returns>包含所有分页信息的数据传递对象</returns>
        PagingEntity<TModel> GetInPagingNoTracking<TModel>(Expression<Func<TEntity, bool>> query, int pageIndex, int pageSize, string orderPropertyName, Expression<Func<TEntity, TModel>> select, bool sort = true) where TModel : class,new();
        /// <summary>
        /// 分页 不做状态跟踪 async
        /// </summary>
        /// <param name="query">where</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页size</param>
        /// <param name="orderPropertyName">order Property</param>
        /// <param name="sort"> 正或逆 </param>
        /// <returns>包含所有分页信息的数据传递对象</returns>
        Task<PagingEntity<TEntity>> GetInPagingNoTrackingAsync(Expression<Func<TEntity, bool>> query, int pageIndex, int pageSize, string orderPropertyName, bool sort = true);
        /// <summary>
        /// 分页 不做状态跟踪 ，并根据select查询指定字段 async
        /// </summary>
        /// <param name="query">where</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页size</param>
        /// <param name="orderPropertyName">order Property</param>
        /// <param name="sort"> 正或逆 </param>
        /// <returns>包含所有分页信息的数据传递对象</returns>
        Task<PagingEntity<TModel>> GetInPagingNoTrackingAsync<TModel>(Expression<Func<TEntity, bool>> query, int pageIndex, int pageSize, string orderPropertyName, Expression<Func<TEntity, TModel>> select, bool sort = true) where TModel : class,new();
    }
}
