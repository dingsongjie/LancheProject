using Lanche.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Lanche.UnitOfWork
{
    /// <summary>
    /// Base for all Unit Of Work classes.
    /// </summary>
    public abstract class UnitOfWorkBase : IUnitOfWork
    {
        public string Id { get; private set; }

        public IUnitOfWork Outer { get; set; }

      
        public event EventHandler Completed;

        
        public event EventHandler<UnitOfWorkFailedEventArgs> Failed;

        
        public event EventHandler Disposed;

        
        public UnitOfWorkOptions Options { get; private set; }

     
   
       
        protected IUnitOfWorkDefaultOptions DefaultOptions { get; private set; }

      
        public bool IsDisposed { get; private set; }

      

       /// <summary>
       /// 是否已经开始
       /// </summary>
        private bool _isBeginCalledBefore;

       /// <summary>
       /// 是否已经完成
       /// </summary>
        private bool _isCompleteCalledBefore;

       /// <summary>
       /// 是否成功
       /// </summary>
        private bool _succeed;

       /// <summary>
       /// 错误信息
       /// </summary>
       
        private Exception _exception;

        /// <summary>
        /// Constructor.
        /// </summary>
        protected UnitOfWorkBase(IUnitOfWorkDefaultOptions defaultOptions)
        {
            DefaultOptions = defaultOptions;
            // 去掉 -
            Id = Guid.NewGuid().ToString("N");
          
          
        }

  
        public void Begin(UnitOfWorkOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException("options");
            }

            PreventMultipleBegin();
            Options = options; 

            BeginUow();
        }


        public abstract void SaveChanges();

      

     

     

        public void Complete()
        {
            PreventMultipleComplete();
            try
            {
                CompleteUow();
                _succeed = true;
                OnCompleted();
            }
            catch (Exception ex)
            {
                _exception = ex;
                throw;
            }
        }

       

    
        public void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }

            IsDisposed = true;

            if (!_succeed)
            {
                OnFailed(_exception);
            }

            DisposeUow();
            OnDisposed();
        }

     
        protected abstract void BeginUow();

      
        protected abstract void CompleteUow();

      

        protected abstract void DisposeUow();

      

        /// <summary>
        /// 触发Completed事件
        /// </summary>
        protected virtual void OnCompleted()
        {
           
            if(Completed!=null)
            {
                Completed(this, EventArgs.Empty);
            }
        }

       /// <summary>
       /// 触发failed事件
       /// </summary>
       /// <param name="exception"></param>
        protected virtual void OnFailed(Exception exception)
        {
            if(Failed!=null)
            {
                Failed(this, new UnitOfWorkFailedEventArgs(exception));
            }
          
        }

        /// <summary>
        /// 触发 dispose 事件
        /// </summary>
        protected virtual void OnDisposed()
        {
            if (Disposed != null)
            {
                Disposed(this, EventArgs.Empty);
            }
        }
        /// <summary>
        /// 防止多次 begiN
        /// </summary>
        private void PreventMultipleBegin()
        {
            if (_isBeginCalledBefore)
            {
                throw new Exception("uow 已经 begin.");
            }

            _isBeginCalledBefore = true;
        }
        /// <summary>
        /// 防止多次 complete
        /// </summary>
        private void PreventMultipleComplete()
        {
            if (_isCompleteCalledBefore)
            {
                throw new Exception("uow 已经 完成");
            }

            _isCompleteCalledBefore = true;
        }



        public async Task CompleteAsync()
        {
            PreventMultipleComplete();
            try
            {
                await CompleteUowAsync();
                _succeed = true;
                OnCompleted();
            }
            catch (Exception ex)
            {
                _exception = ex;
                throw;
            }
        }
       
        protected abstract Task CompleteUowAsync();
        public abstract  Task SaveChangesAsync();
    }
}
