using Castle.Core;
using Lanche.Core.Dependency;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace Lanche.UnitOfWork
{
    /// <summary>
    /// 逻辑上下文工作单元提供者
    /// </summary>
    public class CallContextCurrentUnitOfWorkProvider : IUnitOfWorkProvider
    {
       

        private const string ContextKey = "UnitOfWork.Current";

        /// <summary>
        /// 工作单元  线程安全
        /// </summary>
        private static readonly ConcurrentDictionary<string, IUnitOfWork> UnitOfWorkDictionary = new ConcurrentDictionary<string, IUnitOfWork>();

       
        /// <summary>
        /// 得到当前的工作单元
        /// </summary>
        /// <returns></returns>
        private static IUnitOfWork GetCurrentUow()
        {
            var unitOfWorkKey = CallContext.LogicalGetData(ContextKey) as string;
            if (unitOfWorkKey == null)
            {
                return null;
            }

            IUnitOfWork unitOfWork;
            if (!UnitOfWorkDictionary.TryGetValue(unitOfWorkKey, out unitOfWork))
            {
               
                CallContext.FreeNamedDataSlot(ContextKey);
                return null;
            }

            if (unitOfWork.IsDisposed)
            {
               
                UnitOfWorkDictionary.TryRemove(unitOfWorkKey, out unitOfWork);
                CallContext.FreeNamedDataSlot(ContextKey);
                throw new Exception("此工作单元已经被dispose!");
               
            }

            return unitOfWork;
        }
        /// <summary>
        /// 存放工作单元
        /// </summary>
        /// <param name="value"></param>
        private static void SetCurrentUow(IUnitOfWork value)
        {
            if (value == null)
            {
                ///删除该 uow
                ExitFromCurrentUowScope();
                return;
            }

            var unitOfWorkKey = CallContext.LogicalGetData(ContextKey) as string;
            if (unitOfWorkKey != null)
            {
                IUnitOfWork outer;
                if (UnitOfWorkDictionary.TryGetValue(unitOfWorkKey, out outer))
                {
                    if (outer == value)
                    {
                        throw new Exception("已经有了该工作单元");
                       
                    }

                    value.Outer = outer;
                }
                else
                {
                   
                }
            }

            unitOfWorkKey = value.Id;
            if (!UnitOfWorkDictionary.TryAdd(unitOfWorkKey, value))
            {
                throw new Exception("字典中添加UnitOfWork失败");
            }

            CallContext.LogicalSetData(ContextKey, unitOfWorkKey);
        }

        private static void ExitFromCurrentUowScope()
        {
            var unitOfWorkKey = CallContext.LogicalGetData(ContextKey) as string;
            if (unitOfWorkKey == null)
            {
                throw new Exception("工作单元Key在逻辑上下文中不存在");
              
            }

            IUnitOfWork unitOfWork;
            if (!UnitOfWorkDictionary.TryGetValue(unitOfWorkKey, out unitOfWork))
            {           
                CallContext.FreeNamedDataSlot(ContextKey);
                return;
            }

            UnitOfWorkDictionary.TryRemove(unitOfWorkKey, out unitOfWork);
            if (unitOfWork.Outer == null)
            {
                CallContext.FreeNamedDataSlot(ContextKey);
                return;
            }

            

            var outerUnitOfWorkKey = unitOfWork.Outer.Id;
            if (!UnitOfWorkDictionary.TryGetValue(outerUnitOfWorkKey, out unitOfWork))
            {
                
                
                CallContext.FreeNamedDataSlot(ContextKey);
                throw new Exception("工作单元不存在");
                
            }

            CallContext.LogicalSetData(ContextKey, outerUnitOfWorkKey);
        }

        /// <summary>
        /// 得到,或设置当前UnitofWork
        /// </summary>
        
        [DoNotWire]
        public IUnitOfWork Current
        {
            get { return GetCurrentUow(); }
            set { SetCurrentUow(value); }
        }
    }
}
