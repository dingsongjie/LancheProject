using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Lanche.Domain.Repository;
using System;
using System.Collections.Generic;
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

        DbRawSqlQuery<TResult> SqlQuery<TResult>(string sql, params object[] parameters);
        int ExecuteSqlCommand(string sql, params object[] parameters);
        Task<int> ExecuteSqlCommandAsync(string sql, params object[] parameters);
        int Delete(Expression<Func<TEntity, bool>> filter);
        Task<int> DeleteAsync(Expression<Func<TEntity, bool>> filter);
        int Update(Expression<Func<TEntity, bool>> filter,Expression<Func<TEntity,TEntity>> update);
        Task<int> UpdateAsync(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TEntity>> update);
        void BulkInsert(IEnumerable<TEntity> entities, SqlBulkCopyOptions options, int? bulkSize=null);

        void BulkInsert(IEnumerable<TEntity> entities, int? bulkSize=null);
    }
}
