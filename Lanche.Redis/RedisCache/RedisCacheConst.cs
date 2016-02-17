using Lanche.Core.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.Redis.RedisCache
{
    public class RedisCacheConst : IMultipleDependency
    {
       public string RedisConnectionName = "Lanche.Redis.Cache";
    }
}
