using Lanche.MongoDB.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.MongoDB.Provider
{
    public class DefaultMongoDbDatabaseProvider<TMongoDbContext> : IMongoDbDatabaseProvider<TMongoDbContext> where TMongoDbContext : MongoDbContext
    {

        private readonly IMongoDbContextProvider<TMongoDbContext> _dbContextProvider;

        public DefaultMongoDbDatabaseProvider(IMongoDbContextProvider<TMongoDbContext> dbContextProvider)
        {
            _dbContextProvider = dbContextProvider;
        }
        public TMongoDbContext DbContext
        {
            get
            {
                return _dbContextProvider.DbContext;

            }
        }

        public global::MongoDB.Driver.IMongoDatabase Database
        {
            get
            {

                return new global::MongoDB.Driver.MongoClient(DbContext.ConnectionString)
                .GetDatabase(DbContext.Database);
            }
        }
    }
}
