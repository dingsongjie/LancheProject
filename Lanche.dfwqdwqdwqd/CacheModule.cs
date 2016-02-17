using Lanche.Core;
using Lanche.Core.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.Cache
{
    [DependsOn(typeof(CoreModule))]
    public class CacheModule:Module
    {
        public override void PreInitialize()
        {
            IocManager.IocContainer.Install(new CacheInstaller());
            base.PreInitialize();
        }
    }
}
