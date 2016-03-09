using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lanche.Domain.Repository.Paging
{
    /// <summary>
    /// 分页数据传输对象
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public  class PagingEntity<TEntity> where TEntity :class ,new()
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public List<TEntity> Entities { get; set; }
        public int EntityTotalCount { get; set; }
       
        public int MaxPage { get; set; }
    }
}
