using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.Core.Module
{
    internal class ModuleCollection : List<ModuleInfo>
    {

        /// <summary>
        /// 获取某个模块的实例
        /// </summary>
        /// <typeparam name="TModule"></typeparam>
        /// <returns></returns>
        public TModule GetModule<TModule>() where TModule : Module
        {
            var module = this.FirstOrDefault(m => m.Type == typeof(TModule));
            if (module == null)
            {
                throw new Exception("Can not find module for " + typeof(TModule).FullName);
            }

            return (TModule)module.Instance;
        }

     /// <summary>
     /// 根据依赖关系给模块集合排序
     /// </summary>
     /// <returns></returns>
        public List<ModuleInfo> GetSortedModuleListByDependency()
        {
            var sortedModules = this.SortByDependencies(x => x.Dependencies);
            EnsureCoreModuleToBeFirst(sortedModules);
            return sortedModules;
        }
        /// <summary>
        /// 把coreModule放到第一个位子
        /// </summary>
        /// <param name="sortedModules"></param>
        private static void EnsureCoreModuleToBeFirst(List<ModuleInfo> sortedModules)
        {
            var kernelModuleIndex = sortedModules.FindIndex(m => m.Type == typeof(CoreModule));
            if (kernelModuleIndex > 0)
            {
                var kernelModule = sortedModules[kernelModuleIndex];
                sortedModules.RemoveAt(kernelModuleIndex);
                sortedModules.Insert(0, kernelModule);
            }
        }
       /// <summary>
       /// 根据依赖关系排序
       /// </summary>
       /// <param name="getDependencies"></param>
       /// <returns></returns>
        public List<ModuleInfo> SortByDependencies(Func<ModuleInfo, IEnumerable<ModuleInfo>> getDependencies)
        {


            var sorted = new List<ModuleInfo>();
            var visited = new Dictionary<ModuleInfo, bool>();

            foreach (var item in this)
            {
                SortByDependenciesVisit(item, getDependencies, sorted, visited);
            }

            return sorted;
        }

       /// <summary>
       /// 更具依赖关系排序
       /// </summary>
       /// <typeparam name="T"></typeparam>
       /// <param name="item"></param>
       /// <param name="getDependencies"></param>
       /// <param name="sorted"></param>
       /// <param name="visited"></param>
        private  void SortByDependenciesVisit<T>(T item, Func<T, IEnumerable<T>> getDependencies, List<T> sorted, Dictionary<T, bool> visited)
        {
            bool inProcess;
            var alreadyVisited = visited.TryGetValue(item, out inProcess);

            if (alreadyVisited)
            {
                if (inProcess)
                {
                    throw new ArgumentException("循环依赖");
                }
            }
            else
            {
                visited[item] = true;

                var dependencies = getDependencies(item);
                if (dependencies != null)
                {
                    foreach (var dependency in dependencies)
                    {
                        SortByDependenciesVisit(dependency, getDependencies, sorted, visited);
                    }
                }

                visited[item] = false;
                sorted.Add(item);
            }
        }
    }
}
