﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lanche.Core.Domain.Repository.Paging
{
    /// <summary>
    /// 分页数据传输对象
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public  class PagingDTO<TEntity> where TEntity :class ,new()
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public List<TEntity> Entities { get; set; }
       
        public int MaxPage { get; set; }
    }
}
