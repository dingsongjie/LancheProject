﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.Domain.Repository.Entity.Auditing
{
   public interface IUpdationAudited<TPrimaryKey>
    {
        TPrimaryKey UpdateUesrId { get; set; }
        DateTime UpdateTime { get; set; }
    }
}
