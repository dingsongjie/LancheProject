using Lanche.Core.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.MongoDB.DbContext
{
    public class DefaultMongoDbContextProvider<TMongoDbContext> : IMongoDbContextProvider<TMongoDbContext> where TMongoDbContext : MongoDbContext
    {
        public TMongoDbContext DbContext
        {
            get { return IocManager.Instance.Resolve<TMongoDbContext>(); }
        }
    }
}
