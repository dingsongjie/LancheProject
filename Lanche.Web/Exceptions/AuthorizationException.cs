using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.Web.Exceptions
{
    public class FriendlyException : Exception
    {
        
        public FriendlyException(string friendlyMessage):base(friendlyMessage)
        {
           
        }
        public FriendlyException(string friendlyMessage,Exception exception):base(friendlyMessage,exception)
        {
  
             
        }
    }
}
