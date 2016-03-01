using Lanche.Core.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lanche.Domain.Repository
{
    /// <summary>
    /// 起到反射后的筛选作用 ，表示 这是一个数据访问仓库
    /// </summary>
    public interface IRepository : ITransientDependency
    {
    }
}
