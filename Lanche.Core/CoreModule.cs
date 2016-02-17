using Lanche.Core.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.Core
{
    public class CoreModule :Lanche.Core.Module. Module
    {
        public override void PreInitialize()
        {
            this.AllAssemblies = AssemblyFinder.GetAllAssemblies();
          
            base.PreInitialize();
        }
        public override void Initialize()
        {
            DefaultConventionalRegistrar.Initialize(IocManager, this.AllAssemblies);
            base.Initialize();
        }
    }
}
