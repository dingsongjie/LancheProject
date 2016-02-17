using Lanche.Core.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.UnitOfWork
{
    public interface IUnitOfWork : ITransientDependency,IDisposable
    {
        /// <summary>
        /// 此工作单元Id 标识
        /// </summary>
        string Id { get; }

        /// <summary>
        /// 外部 unit of work 的引用
        /// </summary>
        IUnitOfWork Outer { get; set; }

        /// <summary>
        /// 工作单元开始执行
        /// </summary>
        /// <param name="options">Unit of work options</param>
        void Begin(UnitOfWorkOptions options);
       /// <summary>
       /// 工作单元完成时的工作
       /// </summary>
        void Complete();
        /// <summary>
        /// 工作单元完成时的工作 异步
        /// </summary>
        Task CompleteAsync();

        /// <summary>
        /// 完成时触发
        /// </summary>
        event EventHandler Completed;

        /// <summary>
        /// 失败时触发
        /// </summary>
        event EventHandler<UnitOfWorkFailedEventArgs> Failed;

        /// <summary>
        /// Dispose时 触发
        /// </summary>
        event EventHandler Disposed;

        /// <summary>
        /// unitofwork 一些选项配置
        /// </summary>
        UnitOfWorkOptions Options { get; }



        /// <summary>
        /// 是否被Dispose
        /// </summary>
        bool IsDisposed { get; }

        /// <summary>
        /// 保存工作单元的所有更改
        /// </summary>
        void SaveChanges();


        Task SaveChangesAsync();


      
        
    }
}
