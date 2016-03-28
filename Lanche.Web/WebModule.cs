using Lanche.Core;
using Lanche.Core.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Lanche.Web
{
    /// <summary>
    /// This module is used to use ABP in ASP.NET web applications.
    /// </summary>
    [DependsOn(typeof(CoreModule))]
    public class WebModule : Module
    {

    }
}
