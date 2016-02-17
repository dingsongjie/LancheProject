using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.Core.Module
{
    internal interface IModuleFinder
    {
        List<Type> FindAll();
    }
}
