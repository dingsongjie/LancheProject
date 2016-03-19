using Lanche.Domain.Repository.Paging;
using Lanche.DynamicWebApi.Controller.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.Domain.Repository.Paging
{
    public class PagingResultEntity<TEntity>:AjaxResponse where TEntity:class,new()
    {
        public PagingResultEntity(PagingEntity<TEntity> pagingEntity,int draw)
        {
            this.draw = draw;
            this.recordsTotal = pagingEntity.EntityTotalCount;
            this.recordsFiltered = pagingEntity.EntityTotalCount;
            this.Data = pagingEntity.Entities;
            
        }
        public int draw { get;private set; }
        public long recordsTotal { get; private set; }
        public int recordsFiltered { get; private set; }
    }
}
