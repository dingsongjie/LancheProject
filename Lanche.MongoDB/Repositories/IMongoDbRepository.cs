using Lanche.Core.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.MongoDB.Repositories
{
    public interface IMongoDbRepository<TEntity> : IRepository<TEntity>, IOutOfUnitOfWork where TEntity : class,new()
    {
        void InsertMany(IEnumerable<TEntity> entities);
        Task InsertManyAsync(IEnumerable<TEntity> entities);
        int Update(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TEntity>> update);
        Task<int> UpdateAsync(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TEntity>> update);
        int Delete(Expression<Func<TEntity, bool>> filter);
        Task<int> DeleteAsync(Expression<Func<TEntity, bool>> filter);
    }
}
