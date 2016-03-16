using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.Domain.Repository.Entity
{
    public class EntityBase<TPrimaryKey>
    {
        public TPrimaryKey Id { get; set; }
        
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is EntityBase<TPrimaryKey>))
            {
                return false;
            }
            //引用相同当然为true
            if (ReferenceEquals(this, obj))
            {
                return true;
            }        
            var other = (EntityBase<TPrimaryKey>)obj;                  
            var typeOfThis = GetType();
            var typeOfOther = other.GetType();
            if (!typeOfThis.IsAssignableFrom(typeOfOther) && !typeOfOther.IsAssignableFrom(typeOfThis))
            {
                return false;
            }

            return Id.Equals(other.Id);
        }
        /// <summary>
        /// int 只作为hash 所以千万不要把Entity当做Key存到 hash类型容器中
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
        public static bool operator ==(EntityBase<TPrimaryKey> left, EntityBase<TPrimaryKey> right)
        {
            if (Equals(left, null))
            {
                return Equals(right, null);
            }

            return left.Equals(right);
        }
        public static bool operator !=(EntityBase<TPrimaryKey> left, EntityBase<TPrimaryKey> right)
        {
            return !(left == right);
        }

       
        public override string ToString()
        {
            return string.Format("[{0} {1}]", GetType().Name, Id);
        }
    }
}
