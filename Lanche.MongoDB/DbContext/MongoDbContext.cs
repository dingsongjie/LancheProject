using Lanche.Core.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.MongoDB.DbContext
{
    public abstract class MongoDbContext :Lanche.Core.Dependency.ITransientDependency
    {
        private static string CONNECTION_STRING;
        public string ConnectionString { get { return CONNECTION_STRING; } }
        public  string Database { get; private set; }


        public MongoDbContext(string connectionName,string databaseName)
        {
            if (string.IsNullOrEmpty(connectionName))
            {
                throw new ArgumentNullException("连接字符串不能为空", "connectionName");
            }
            if (string.IsNullOrEmpty(databaseName))
            {
                throw new ArgumentNullException("数据库名称不能为空", "DatbaseName");
            }
          
            if (string.IsNullOrEmpty(CONNECTION_STRING))
            {
                var configuration = System.Configuration.ConfigurationManager.ConnectionStrings[connectionName];
                if (configuration == null)
                {
                    throw new Exception("配置文件中没有找到指定的连接字符串,连接字符串名称:" + connectionName);
                }
                CONNECTION_STRING = configuration.ConnectionString;
            }

            Database = databaseName;
            
        }
        public static IEnumerable<Type> GetEntityTypes( Type type)
        {
            return
               from property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
               where
                   ReflectionHelper.IsAssignableToGenericType(property.PropertyType, typeof(MongoDbSet<>))
               select property.PropertyType.GetGenericArguments()[0];
        }

    }
}
