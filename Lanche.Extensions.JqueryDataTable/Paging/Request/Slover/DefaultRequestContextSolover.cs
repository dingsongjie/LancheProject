using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.Domain.Repository.Paging
{
    public class DefaultPagingRequestEntitySlove : IPagingRequestEntitySlover
    {

        public PagingParsedEntity<TEntity> Slover<TEntity>(PagingRequestEntity<TEntity> entity) where TEntity:class,new()
        {
            try
            {
                var pageIndex = entity.Start / entity.Length + 1;
                var ordercolumn = entity.Order[0].Column;
                string orderName = entity.Columns[ordercolumn].Data;
                bool desc = entity.Order[0].Dir == "asc" ? true : false;
                if(entity.Entity==null)
                {
                    entity.Entity = new TEntity();
                }
                PagingParsedEntity<TEntity> result = new PagingParsedEntity<TEntity>()
                {
                    Query = entity.Entity,
                    OrderName = orderName,
                    PageIndex = pageIndex,
                    PageSize = entity.Length,
                    Sort = desc
                };
                return result;
            }
            catch(Exception e)
            {
                throw new Exception("分页传入参数有误", e);
            }
            
        }
    }
}
