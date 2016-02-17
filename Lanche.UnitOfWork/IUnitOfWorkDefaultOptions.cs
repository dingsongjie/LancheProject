using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace Lanche.UnitOfWork
{
    /// <summary>
    /// unitofwork 默认选项
    /// </summary>
     public interface IUnitOfWorkDefaultOptions
    {
        /// <summary>
        /// 事物选项
        /// </summary>
        TransactionScopeOption Scope { get; set; }

      /// <summary>
      /// 是否开启事务，默认 false
      /// </summary>
        bool IsTransactional { get; set; }

        /// <summary>
        /// 事物 timeout时间
        /// </summary>
        TimeSpan? Timeout { get; set; }

        /// <summary>
        /// 事务隔离界别
        /// </summary>
        IsolationLevel? IsolationLevel { get; set; }

    


    }
}
