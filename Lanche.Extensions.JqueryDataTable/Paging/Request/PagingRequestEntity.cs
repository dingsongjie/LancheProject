using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.Domain.Repository.Paging
{
    public class PagingRequestEntity<TEntity> where TEntity : class,new()
    {
       public List<Column> Columns { get; set; }
       public List<Order> Order { get; set; }
       public int Start { get; set; }
       public int Length { get; set; }
       public Search Search { get; set; }
       public int Draw { get; set; }
       public TEntity Entity { get; set; }

    }
}
