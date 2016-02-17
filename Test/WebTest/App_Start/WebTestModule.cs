using Lanche.Core.Module;
using Lanche.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebTest.App_Start
{
    [DependsOn(typeof(WebMvcModule))]
    public class WebTestModule : Module
    {

    }
}