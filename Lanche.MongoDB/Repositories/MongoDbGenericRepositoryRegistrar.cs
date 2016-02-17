using Lanche.Core.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lanche.MongoDB.Extensions;
using Lanche.MongoDB.DbContext;
namespace Lanche.MongoDB.Repositories
{
   public static  class MongoDbGenericRepositoryRegistrar
    {
       public static void RegisterForDbContext(Type dbContextType, IIocManager iocManager)
       {
           var autoRepositoryAttr = dbContextType.GetSingleAttributeOrNull<AutoRepositoryTypesAttribute>();
           if (autoRepositoryAttr == null)
           {
               autoRepositoryAttr = AutoRepositoryTypesAttribute.Default;
           }

           foreach (var entityType in MongoDbContext.GetEntityTypes( dbContextType))
           {

               var genericRepositoryType = autoRepositoryAttr.RepositoryInterface.MakeGenericType(entityType);
               if (!iocManager.IsRegistered(genericRepositoryType))
               {
                   var implType = autoRepositoryAttr.RepositoryImplementation.GetGenericArguments().Length == 1
                           ? autoRepositoryAttr.RepositoryImplementation.MakeGenericType(entityType)
                           : autoRepositoryAttr.RepositoryImplementation.MakeGenericType(dbContextType, entityType);

                   iocManager.Register(
                       genericRepositoryType,
                       implType,
                       DependencyLifeStyle.Transient
                       );
               }

           }
       }
    }
}
