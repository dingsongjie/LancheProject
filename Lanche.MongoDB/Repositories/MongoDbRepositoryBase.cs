using Lanche.Domain.Repository;

using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver.Linq;
using System.Reflection;
using Lanche.Domain.Repository.Paging;
using Lanche.MongoDB.DbContext;
using Lanche.MongoDB.Provider;

namespace Lanche.MongoDB.Repositories
{
    public class MongoDbRepositoryBase<TMongoDbContext, TEntity> : RepositoryBase<TEntity>, IMongoDbRepository<TEntity>
        where TEntity : class,new()
        where TMongoDbContext : MongoDbContext
    {
        private readonly IMongoDbDatabaseProvider<TMongoDbContext> _databaseProvider;

        protected IMongoDatabase Database
        {
            get { return _databaseProvider.Database; }
        }

        protected IMongoCollection<TEntity> Collection
        {
            get
            {
                return _databaseProvider.Database.GetCollection<TEntity>(typeof(TEntity).Name);
            }
        }

        public MongoDbRepositoryBase(IMongoDbDatabaseProvider<TMongoDbContext> databaseProvider)
        {
            _databaseProvider = databaseProvider;
        }

        public override IQueryable<TEntity> GetAll()
        {
            return Collection.AsQueryable();
        }


        public override TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {

            return Collection.Find(predicate).FirstOrDefault();

        }
        public async override Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Collection.Find(predicate).FirstOrDefaultAsync();
        }

        public override TEntity Insert(TEntity entity)
        {
            Collection.InsertOne(entity);
            return entity;
        }
        public async override Task<TEntity> InsertAsync(TEntity entity)
        {
            await Collection.InsertOneAsync(entity);
            return entity;
        }
        [Obsolete("这个方法不可用,请用带两个参数 的 update方法")]
        public override TEntity Update(TEntity entity)
        {
            throw new Exception("这个方法不可用");
        }
        [Obsolete("这个方法不可用,请用重载的 参数为 表达式 的 delete方法")]
        public override void Delete(TEntity entity)
        {

            throw new Exception("这个方法不可用");
        }
        public async override Task<int> CountAsync()
        {
            var value = await Collection.CountAsync(FilterDefinition<TEntity>.Empty);
            if (value > int.MaxValue)
            {
                throw new OverflowException("数量超过int32的最大值");
            }
            return (int)value;
        }
        public async override Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var value = await Collection.CountAsync(predicate);
            if (value > int.MaxValue)
            {
                throw new OverflowException("数量超过int32的最大值");
            }
            return (int)value;
        }
        [Obsolete("这个方法不可用,请用重载的 参数为 表达式 的 delete方法")]
        public override Task DeleteAsync(TEntity entity)
        {
            throw new Exception("这个方法不可用");
        }
        public async override Task<List<TEntity>> GetAllListAsync()
        {
            return await (await Collection.FindAsync(FilterDefinition<TEntity>.Empty)).ToListAsync();
        }
        public async override Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await (await Collection.FindAsync(predicate)).ToListAsync();
        }

        public async override Task<long> LongCountAsync()
        {
            return await Collection.CountAsync(FilterDefinition<TEntity>.Empty);
        }
        public async override Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Collection.CountAsync(predicate);
        }
        public async override Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Collection.Find(predicate).SingleAsync();
        }





        public int Update(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TEntity>> update)
        {
            UpdateDefinition<TEntity> lists = Newtonsoft.Json.JsonConvert.SerializeObject(update);

            var result = Collection.UpdateMany(filter, lists);
            if (result.ModifiedCount > int.MaxValue)
            {
                throw new Exception("更新的行数超过了 int32的最大值");
            }
            return (int)result.ModifiedCount;
        }

        public async Task<int> UpdateAsync(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TEntity>> update)
        {
            UpdateDefinition<TEntity> lists = Newtonsoft.Json.JsonConvert.SerializeObject(update);

            var result = await Collection.UpdateManyAsync(filter, lists);
            if (result.ModifiedCount > int.MaxValue)
            {
                throw new Exception("更新的行数超过了 int32的最大值");
            }
            return (int)result.ModifiedCount;
        }

        public int Delete(Expression<Func<TEntity, bool>> filter)
        {
            var result = Collection.DeleteMany(filter);
            if (result.DeletedCount > int.MaxValue)
            {
                throw new Exception("删除的行数超过了 int32的最大值");
            }
            return (int)result.DeletedCount;
        }

        public async Task<int> DeleteAsync(Expression<Func<TEntity, bool>> filter)
        {
            var result = await Collection.DeleteManyAsync(filter);
            if (result.DeletedCount > int.MaxValue)
            {
                throw new Exception("删除的行数超过了 int32的最大值");
            }
            return (int)result.DeletedCount;
        }

        public void InsertMany(IEnumerable<TEntity> entities)
        {
            Collection.InsertMany(entities);
        }

        public async Task InsertManyAsync(IEnumerable<TEntity> entities)
        {
            await Collection.InsertManyAsync(entities);
        }
    }
}
