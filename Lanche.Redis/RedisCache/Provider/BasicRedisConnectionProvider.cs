using StackExchange.Redis;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.Redis.RedisCache.Provider
{
    public class BasicRedisConnectionProvider : IRedisConnectionProvider
    {
        private static readonly ConcurrentDictionary<string, Lazy<ConnectionMultiplexer>> ConnectionMultiplexers = new ConcurrentDictionary<string, Lazy<ConnectionMultiplexer>>();

        public string GetConnectionString(string redisConnectionName)
        {
            var connectionStringSettings = ConfigurationManager.ConnectionStrings[redisConnectionName];

            if (connectionStringSettings == null)
            {
                throw new Exception("连接字符串未找到： " + redisConnectionName);
            }

            return connectionStringSettings.ConnectionString;
        }

        public ConnectionMultiplexer GetConnection(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException("connectionString");
            }

            ///  Lazy   参考 azure redis

            return ConnectionMultiplexers.GetOrAdd(
                connectionString,
                new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(connectionString))
                ).Value;
        }
    }
}
