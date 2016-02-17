using Lanche.Core.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.DynamicWebApi.Controller
{
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
            _manager.Release(Object);
        }
    }
}
