using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace Lanche.UnitOfWork
{
    /// <summary>
    /// 默认 uow 选项
    /// </summary>
    public class UnitOfWorkDefaultOptions : IUnitOfWorkDefaultOptions
    {
        public TransactionScopeOption Scope { get; set; }

        /// <inheritdoc/>
        public bool IsTransactional { get; set; }

        /// <inheritdoc/>
        public TimeSpan? Timeout { get; set; }

        /// <inheritdoc/>
        public IsolationLevel? IsolationLevel { get; set; }

     
        public UnitOfWorkDefaultOptions()
        {
           
            IsTransactional = false;
            Scope = TransactionScopeOption.Required;
        }
    }
}
