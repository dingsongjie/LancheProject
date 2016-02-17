using Lanche.Core.Reflection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Helper
{
    class TestAssemblyFinder : IAssemblyFinder
    {
        public List<System.Reflection.Assembly> GetAllAssemblies()
        {
            List<Assembly> assemblies = new List<Assembly>();
            SearchOption FindAssembliesSearchOption = SearchOption.TopDirectoryOnly;
            var assembliesInBinFolder = new List<Assembly>();


            var dllFiles = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.dll", FindAssembliesSearchOption).ToList();

            foreach (string dllFile in dllFiles)
            {
                assemblies.Add(Assembly.LoadFile(dllFile));

            }
            return assemblies;
        }
    }
}
