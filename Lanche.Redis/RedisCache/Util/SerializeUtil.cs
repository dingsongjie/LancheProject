using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.Redis.RedisCache.Util
{
   public class SerializeUtil
    {
        public static byte[] Serialize(object data)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream rems = new MemoryStream();
            formatter.Serialize(rems, data);
            return rems.GetBuffer();
        }

        public static object Deserialize(byte[] data)
        {
            using (var memoryStream = new MemoryStream(data))
            {
                return new BinaryFormatter().Deserialize(memoryStream);
            }
        }

     
    }
}
