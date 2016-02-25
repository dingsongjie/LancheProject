using Lanche.Core.Configuration;
using Lanche.Core.Dependency;
using Lanche.Core.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.Core.Module
{
    public abstract class Module
    {
       /// <summary>
       /// ioc
       /// </summary>
        protected internal IIocManager IocManager { get; internal set; }

      
        public IAssemblyFinder AssemblyFinder { get; set; }
        /// <summary>
        /// configuration manager
        /// </summary>
        protected internal IConfigurationManager ConfigurationManager { get; internal set; }
        public List<Assembly> AllAssemblies { get; set; }
     

  
        public virtual void PreInitialize()
        {
           
        }

     
        public virtual void Initialize()
        {

        }

        public virtual void PostInitialize()
        {

        }

        /// <summary>
        /// 卸载
        /// </summary>
        public virtual void Shutdown()
        {

        }

        
        public static bool IsModule(Type type)
        {
            return
                type.IsClass &&
                !type.IsAbstract &&
                typeof(Module).IsAssignableFrom(type);
        }

       /// <summary>
       /// 得到该模块依赖的模块
       /// </summary>
       /// <param name="moduleType"></param>
       /// <returns></returns>
        public static List<Type> FindDependedModuleTypes(Type moduleType)
        {
            if (!IsModule(moduleType))
            {
                throw new Exception("当前类型不是想要的模块: " + moduleType.AssemblyQualifiedName);
            }

            var list = new List<Type>();

            if (moduleType.IsDefined(typeof(DependsOnAttribute), true))
            {
                var dependsOnAttributes = moduleType.GetCustomAttributes(typeof(DependsOnAttribute), true).Cast<DependsOnAttribute>();
                foreach (var dependsOnAttribute in dependsOnAttributes)
                {
                    foreach (var dependedModuleType in dependsOnAttribute.DependedModuleTypes)
                    {
                        list.Add(dependedModuleType);
                    }
                }
            }

            return list;
        }
    }
}
