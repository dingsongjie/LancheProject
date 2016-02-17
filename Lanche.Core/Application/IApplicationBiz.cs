using Lanche.Core.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Lanche.Core.Application
{
    /// <summary>
    /// 起到反射后的筛选作用，表示这是业务逻辑载体
    /// </summary>
    public interface IApplicationBiz : ITransientDependency
    {
    }
}
