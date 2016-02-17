using Lanche.Entityframework.UnitOfWork.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lanche.Entityframework.UnitOfWork;

namespace UnitTest.Repository
{
    public  class TestRepositoryBase<TEntity> : EfRepositoryBase<TestDbContext,TEntity> where TEntity:class,new()
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public TestRepositoryBase(IDbContextProvider<TestDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
            
        }

    }
}
