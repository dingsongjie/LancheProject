using Lanche.Core;
using Lanche.Core.Dependency;
using Lanche.Core.Module;
using Lanche.Core.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Lanche.UnitOfWork
{
    /// <summary>
    /// 工作单元初始化
    /// </summary>
    [DependsOn(typeof(CoreModule))]
    public class UnitOfWorkMudule : Lanche.Core.Module. Module
    {
       
        public override void Initialize()
        {       
           
        }
      
        /// <summary>
        /// 模块初始化前写入 UnitOfWorkInstaller
        /// </summary>
        public override void PreInitialize()
        {
            IocManager.IocContainer.Install(new UnitOfWorkInstaller());
            UnitOfWorkRegistrar.Initialize(IocManager);
        }
    }
}
