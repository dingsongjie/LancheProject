using Lanche.Core.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.Domain.Repository.Paging
{
    public interface IPagingRequestEntitySolover 
    {
        PagingParsedEntity<TEntity> Slover<TEntity>(PagingRequestEntity<TEntity> entity) where TEntity : class,new();
    }
}
