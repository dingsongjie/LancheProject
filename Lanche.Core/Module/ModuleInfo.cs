using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.Core.Module
{
   internal class ModuleInfo
    {
        /// <summary>
        /// 模块所在程序集
        /// </summary>
        public Assembly Assembly { get; private set; }

      /// <summary>
      /// 模块类Type
      /// </summary>
        public Type Type { get; private set; }

      /// <summary>
      /// 该模块实例
      /// </summary>
        public Module Instance { get; private set; }

        /// <summary>
        /// 依赖的模块
        /// </summary>
        public List<ModuleInfo> Dependencies { get; private set; }

        /// <summary>
        /// Creates a new AbpModuleInfo object.
        /// </summary>
        /// <param name="instance"></param>
        public ModuleInfo(Module instance)
        {
            Dependencies = new List<ModuleInfo>();
            Type = instance.GetType();
            Instance = instance;
            Assembly = Type.Assembly;
        }

        public override string ToString()
        {
            return string.Format("{0}", Type.AssemblyQualifiedName);
        }
    }
}
