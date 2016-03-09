using Lanche.Abstractions.MessageQueue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.MessageQueue
{
    public class DefaultMqConnectionSlover : IMqConnectionSlover
    {
        private string _connectionName;
        public ConnectionInfo GetConnectionInfo(string connectionName)
        {
           if(connectionName==null)
           {
               throw new Exception("连接字符串名称不能为null");
           }
           _connectionName = connectionName;
           var connection = System.Configuration.ConfigurationManager.ConnectionStrings[connectionName];
            if(connection==null)
            {
                throw new Exception("名称为:" + connectionName + "的连接字符串不存在");
            }
            var connectionString = connection.ConnectionString;
            return GetInfo(connectionString);
        }
        private ConnectionInfo GetInfo(string connectionString)
        {
            string[] properties = connectionString.Split(';');
            IDictionary<string, string> propertyPair = new Dictionary<string, string>();
            foreach(var v in properties)
            {
                var keyValue = v.Split('=');
                if(keyValue.Length!=2)
                {
                    throw new Exception("连接字符串格式出错，" + v + "格式有误");
                }
                var key =keyValue[0].ToLower().Trim();
                var value = keyValue[1].ToLower().Trim();
                propertyPair[key] = value;
            }
            var connectionInfo = new ConnectionInfo()
            {
                Name = _connectionName,
                HostName = propertyPair["hostname"],
                Password = propertyPair["password"],
                Port = Convert.ToInt32(propertyPair["port"]),  /// 转换错误 自然报错
                UserName = propertyPair["username"],
                VirtualHost = propertyPair["virtualhost"]
            };
            return connectionInfo;
            
        }
    }
}
