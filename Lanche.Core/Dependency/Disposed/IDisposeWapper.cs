using Lanche.Core.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.DynamicWebApi.Controller
{
    /// <summary>
    /// 用于包装从IocManager中 手动 resolve 出来的对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class IDisposeWapper<T>:IDisposable where T:class
    {
        private readonly IIocManager _manager;
        public  T Object{get; private set;}
        public IDisposeWapper(IIocManager manager,T obj)
        {
            _manager = manager;
            Object = obj;
        }
        
        public void Dispose()
        {
            _manager.Release(Object);  /// release 即可
        }
    }
}
