using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lanche.Core.Module
{
   /// <summary>
   /// 模块化准备，方便解析模块之间的依赖关系
   /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class DependsOnAttribute : Attribute
    {
        /// <summary>
        /// 依赖的模块
        /// </summary>
        public Type[] DependedModuleTypes { get; private set; }

      
        public DependsOnAttribute(params Type[] dependedModuleTypes)
        {
            DependedModuleTypes = dependedModuleTypes;
        }
    }
}
