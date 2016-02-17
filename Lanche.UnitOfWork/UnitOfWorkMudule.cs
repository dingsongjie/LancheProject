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
      

        public override void PreInitialize()
        {
            IocManager.IocContainer.Install(new UnitOfWorkInstaller());
            UnitOfWorkRegistrar.Initialize(IocManager);
        }
    }
}
