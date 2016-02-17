using Lanche.Core.Reflection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Compilation;

namespace Lanche.Web
{

    /// <summary>
    /// web bin dll finder
    /// </summary>
    public class WebAssemblyFinder : IAssemblyFinder
    {
        /// <summary>
        /// 只需要查找 顶层
        /// </summary>
        public static SearchOption FindAssembliesSearchOption = SearchOption.TopDirectoryOnly;

        /// <summary>
        /// 返回所有所需的 dll
        /// </summary>
        /// <returns></returns>
        public List<Assembly> GetAllAssemblies()
        {
            var assembliesInBinFolder = new List<Assembly>();

            var allReferencedAssemblies = BuildManager.GetReferencedAssemblies().Cast<Assembly>().ToList();
            var dllFiles = Directory.GetFiles(HttpRuntime.AppDomainAppPath + "bin\\", "*.dll", FindAssembliesSearchOption).ToList();

            foreach (string dllFile in dllFiles)
            {
                var locatedAssembly = allReferencedAssemblies.FirstOrDefault(asm => AssemblyName.ReferenceMatchesDefinition(asm.GetName(), AssemblyName.GetAssemblyName(dllFile)));
                if (locatedAssembly != null)
                {
                    assembliesInBinFolder.Add(locatedAssembly);
                }
            }

            return assembliesInBinFolder;
        }
    }
}
