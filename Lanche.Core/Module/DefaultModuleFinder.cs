using Lanche.Core.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.Core.Module
{
   internal class DefaultModuleFinder:IModuleFinder
   {
       private readonly ITypeFinder _typeFinder;

       public DefaultModuleFinder(ITypeFinder typeFinder)
       {
           _typeFinder = typeFinder;
       }

       public List<Type> FindAll()
       {
           return _typeFinder.Find(Module.IsModule).ToList();
       }
    }
}
