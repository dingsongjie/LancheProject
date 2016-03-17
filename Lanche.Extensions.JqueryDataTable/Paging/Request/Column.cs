using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.Domain.Repository.Paging
{
   public class Column
    {
       public string Data { get; set; }
       public string Name { get; set; }
       public bool Searchable { get; set; }
       public bool Orderable { get; set; }
       public Search Search { get; set; }
    }
}
