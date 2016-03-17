using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.Domain.Repository.Paging
{
    public class PagingParsedEntity<TEntity> where TEntity : class,new()
    {
       public TEntity Query { get; set; }
       public int PageIndex { get; set; }
       public int PageSize { get; set; }
       public string OrderName { get; set; }
       public bool Sort { get; set; }
    }
}
