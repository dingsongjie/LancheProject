using Lanche.Core.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Lanche.UnitOfWork
{
    public interface IUnitOfWorkManager : ITransientDependency
    {
        /// <summary>
        /// 当前工作单元.
        /// </summary>
        IUnitOfWork Current { get; }

        /// <summary>
        /// 开始工作单元
        /// </summary>
        /// <returns></returns>
        IUnitOfWork Begin();


        /// <summary>
        /// 开始工作单元
        /// </summary>
        /// <returns></returns>
        IUnitOfWork Begin(TransactionScopeOption scope);


        /// <summary>
        /// 开始工作单元
        /// </summary>
        /// <returns></returns>
        IUnitOfWork Begin(UnitOfWorkOptions options);
    }
}
