using Lanche.Core.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.Cache
{
   /// <summary>
   /// cache manager 单例
   /// </summary>
    public interface ICacheManager :  IDisposable
    {
       /// <summary>
       /// 返回所有缓存对象
       /// </summary>
       /// <returns></returns>
        IReadOnlyList<ICache> GetAllCaches();

        /// <summary>
        /// 得到该缓存，如果未匹配到则创建并返回该缓存
        /// </summary>
        /// <param name="name">缓存名</param>
        /// <returns>cache 对向</returns>
        ICache GetOrCreateCache(string name);
    }
}
