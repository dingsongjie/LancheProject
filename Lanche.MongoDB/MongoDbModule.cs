using Lanche.Core;
using Lanche.Core.Dependency;
using Lanche.Core.Module;
using Lanche.Core.Reflection;
using Lanche.MongoDB.DbContext;
using Lanche.MongoDB.Repositories;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.MongoDB
{
    [DependsOn(typeof(CoreModule))]
    public class MongoDbModule:Module
    {
        public ITypeFinder TypeFinder { get; set; }
        public override void PreInitialize()
        {
          
            base.PreInitialize();
        }
        public override void Initialize()
        {
            RegisterGenericRepositories(IocManager);
            base.Initialize();
        }
        private void RegisterGenericRepositories(IIocManager manager)
        {
            var dbContextTypes =
                TypeFinder.Find(type =>
                    type.IsPublic &&
                    !type.IsAbstract &&
                    type.IsClass &&
                    typeof(MongoDbContext).IsAssignableFrom(type)
                    );

            if (dbContextTypes.Count() <= 0)
            {
                return;   // 没有 mongodbContext

            }
            //dbContextTypes.ToList().ForEach(m => IocManager.Register(m));
            foreach (var dbContextType in dbContextTypes)
            {
                MongoDbGenericRepositoryRegistrar.RegisterForDbContext(dbContextType, manager);
            }
        }

      
    }
}
