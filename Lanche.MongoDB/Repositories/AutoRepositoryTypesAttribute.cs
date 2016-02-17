using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.MongoDB.Repositories
{
    /// <summary>
    /// Repository 的注册类 与 实现类
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class AutoRepositoryTypesAttribute : Attribute
    {
        public static AutoRepositoryTypesAttribute Default { get; private set; }

        public Type RepositoryInterface { get; private set; }



        public Type RepositoryImplementation { get; private set; }



        static AutoRepositoryTypesAttribute()
        {
            Default = new AutoRepositoryTypesAttribute(
                typeof(IMongoDbRepository<>),
                typeof(MongoDbRepositoryBase<,>)

                );
        }

        public AutoRepositoryTypesAttribute(
            Type repositoryInterface,

            Type repositoryImplementation
           )
        {
            RepositoryInterface = repositoryInterface;

            RepositoryImplementation = repositoryImplementation;

        }
    }
}
