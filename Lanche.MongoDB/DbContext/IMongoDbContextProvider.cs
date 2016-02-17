using Lanche.Core.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.MongoDB.DbContext
{
    public interface IMongoDbContextProvider<TMongoDbContext> : ITransientDependency where TMongoDbContext : MongoDbContext
    {
         TMongoDbContext DbContext{get;}
    }
}
