using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.Domain.Repository.Entity.Auditing
{
   public  interface ICreationAudited<TPrimaryKey>
    {
         TPrimaryKey CreateUesrId { get; set; }
          DateTime CreateTime { get; set; }
    }
}
