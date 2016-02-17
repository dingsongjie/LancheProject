using Lanche.MongoDB.DbContext;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.MongoDB.Provider
{
    public interface IMongoDbDatabaseProvider<TMongoDbContext>:Lanche.Core.Dependency.ITransientDependency where TMongoDbContext : MongoDbContext
    {
        TMongoDbContext DbContext { get; }
       IMongoDatabase Database { get; }
    }
}
