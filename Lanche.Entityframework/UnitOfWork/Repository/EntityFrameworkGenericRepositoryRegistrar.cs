using Lanche.Core.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lanche.Entityframework.Extensions;
using Lanche.EntityFramework.Extensions;
namespace Lanche.Entityframework.UnitOfWork.Repository
{
    /// <summary>
    /// 为 每个实体 提供默认的 Repository
    /// </summary>
    public static class EntityFrameworkGenericRepositoryRegistrar
    {
        public static void RegisterForDbContext(Type dbContextType, IIocManager iocManager)
        {
            var autoRepositoryAttr = dbContextType.GetSingleAttributeOrNull<AutoRepositoryTypesAttribute>();
            if (autoRepositoryAttr == null)
            {
                autoRepositoryAttr = AutoRepositoryTypesAttribute.Default;
            }

            foreach (var entityType in dbContextType.GetEntityTypes())
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
