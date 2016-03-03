using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Lanche.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Optimization;

[assembly: OwinStartup(typeof(WebTest.Startup))]
namespace WebTest
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseLancheProject();
        }


    }
}
