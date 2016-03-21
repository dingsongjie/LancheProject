using Lanche.Core;
using Lanche.Core.Dependency;
using Lanche.Core.Module;
using Lanche.Core.Reflection;
using Lanche.Entityframework.UnitOfWork;
using Lanche.Entityframework.UnitOfWork.Repository;
using Lanche.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Lanche.Entityframework
{
    /// <summary>
    /// ef 模块的初始化
    /// </summary>
    [DependsOn(typeof(UnitOfWorkMudule), typeof(CoreModule))]
     public class EntityframeworkMudule  : Lanche.Core.Module. Module
    {
     
        public ITypeFinder TypeFinder { get; set; }
        public override void Initialize()
        {
            IocManager.Register(
               typeof(IDbContextProvider<>),
                    typeof(UnitOfWorkDbContextProvider<>), DependencyLifeStyle.Multiple

                    );
          
            RegisterGenericRepositories(IocManager);
           
        }
       
         private void RegisterGenericRepositories(IIocManager manager)
         {
             var dbContextTypes =
                 TypeFinder.Find(type =>
                     type.IsPublic &&
                     !type.IsAbstract &&
                     type.IsClass &&
                     typeof(DbContext).IsAssignableFrom(type)
                     );

             if (dbContextTypes.Count() <= 0)
             {
                 throw new Exception("No class found derived from DbContext.");

             }
             dbContextTypes.ToList().ForEach(m => IocManager.Register(m,DependencyLifeStyle.Multiple));
             foreach (var dbContextType in dbContextTypes)
             {
                 EntityFrameworkGenericRepositoryRegistrar.RegisterForDbContext(dbContextType, manager);
             }
         }

      
    }
}
