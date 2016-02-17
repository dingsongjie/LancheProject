using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lanche.Core.Reflection
{
    /// <summary>
    /// 具有查找类型能力
    /// </summary>
    public interface ITypeFinder
    {
        Type[] Find(Func<Type, bool> where);

        Type[] FindAll();
    }
}
