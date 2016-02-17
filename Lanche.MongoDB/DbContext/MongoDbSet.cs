using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.MongoDB.DbContext
{
    /// <summary>
    /// mongodb  实体容器 ，只是为了泛型注入而用
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class MongoDbSet<TEntity>  where TEntity : class,new()
    {

    }
}
