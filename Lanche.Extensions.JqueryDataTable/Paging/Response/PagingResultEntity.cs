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
            this.Draw = draw;
            this.RecordsTotal = pagingEntity.EntityTotalCount;
            this.RecordsFiltered = pagingEntity.EntityTotalCount;
            this.Data = pagingEntity.Entities;

        }
        public int Draw { get;private set; }
        public long RecordsTotal { get; private set; }
        public int RecordsFiltered { get; private set; }
    }
}
