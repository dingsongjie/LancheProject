using Lanche.Domain.Repository.Entity;
using Lanche.Domain.Repository.Entity.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.Domain.Repository
{
   public static class IRepositoryExtension
    {
        /// <summary>
        /// 新增或更新 ,根据传入实体的Id去数据库查询 若有Id相同的数据则自动判断为更新，否则则为新加，和数据库交互为2次，效率不高，若要比较高的效率请用 Insert 或Update方法
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="repository"></param>
        /// <param name="entity"></param>
       public async static Task<TEntity> InsertOrUpdateAsync<TEntity>(this IRepository<TEntity> repository, TEntity entity) where TEntity :EntityBase<string>, ICreationAudited<string> , new()
        {



            var dbEntity = await repository.FirstOrDefaultAsync(m => m.Id == entity.Id);
            //新增
            if (dbEntity == null)
            {

                return await repository.InsertAsync(entity);
            }
            else
            {

                var result = OverRideProperties(dbEntity, entity);
                return await repository.UpdateAsync(result);
            }
        }
       /// <summary>
       /// 新增或更新 ,根据传入实体的Id去数据库查询 若有Id相同的数据则自动判断为更新，否则则为新加，和数据库交互为2次，效率不高，若要比较高的效率请用 Insert 或Update方法
       /// </summary>
       /// <typeparam name="TEntity"></typeparam>
       /// <param name="repository"></param>
       /// <param name="entity"></param>
       public  static TEntity InsertOrUpdate<TEntity>(this IRepository<TEntity> repository, TEntity entity) where TEntity : EntityBase<string>,ICreationAudited<string>, new()
       {
     
           var dbEntity =  repository.FirstOrDefault(m=>m.Id==entity.Id);
           //新增
           if (dbEntity == null)
           {

               return  repository.Insert(entity);
           }
           else
           {

               var result = OverRideProperties(dbEntity, entity);
               return  repository.Update(result);
           }
       }
       private static TEntity OverRideProperties<TEntity>(TEntity entityDb, TEntity entityNew) where TEntity : ICreationAudited<string>, new()
       {
           var type = entityDb.GetType();
           var properties = type.GetProperties();
           foreach (var pro in properties)
           {
               pro.SetValue(entityDb, pro.GetValue(entityNew));
           }
           return entityDb;
       }
    }
}
