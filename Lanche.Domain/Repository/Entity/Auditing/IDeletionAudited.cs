using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.Domain.Repository.Entity.Auditing
{
   public interface IDeletionAudited<TPrimaryKey>
    {
        TPrimaryKey DeleteUesrId { get; set; }
        DateTime DeleteTime { get; set; }
    }
}
