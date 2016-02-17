using Lanche.Core.Dependency;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.Redis.RedisCache.Provider
{
    public interface IRedisConnectionProvider:ITransientDependency
    {
        ConnectionMultiplexer GetConnection(string connectionString);

        string GetConnectionString(string service);
    }
}
