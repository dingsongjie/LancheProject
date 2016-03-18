using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.Domain.Repository.Entity.Auditing
{
    /// <summary>
    /// 软删除，添加，删除，更新审计信息表基类
    /// </summary>
    public class Entity : EntityBase<string>, ISoftDelete, ICreationAudited<string>, IDeletionAudited<string>, IUpdationAudited<string>
    {
        public DateTime CreateTime
        {
            get;
            set;
        }

        public string CreateUesrId
        {
            get;
            set;
        }

        public DateTime DeleteTime
        {
            get;
            set;
        }

        public string DeleteUesrId
        {
            get;
            set;
        }

        public bool IsDeleted
        {
            get;
            set;
        }

        public DateTime UpdateTime
        {
            get;
            set;
        }

        public string UpdateUesrId
        {
            get;
            set;
        }
    }
}
