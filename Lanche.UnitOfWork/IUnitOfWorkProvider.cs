using Lanche.Core.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lanche.UnitOfWork
{
    /// <summary>
    /// 工作单元提供者
    /// </summary>
    public interface IUnitOfWorkProvider : ITransientDependency
    {
        IUnitOfWork Current { get; set; }
    }
}
