using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Lanche.Core.Reflection
{
   
      /// <summary>
      /// 具有得到程序集的能力
      /// </summary>
        public interface IAssemblyFinder
        {
            /// <summary>
            /// 得到所有有用程序集
            /// </summary>
            /// <returns>List of assemblies</returns>
            List<Assembly> GetAllAssemblies();
       }
    
}
