using Lanche.Core;
using Lanche.Core.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Lanche.Web
{
    /// <summary>
    /// Http Application 基类
    /// </summary>
    public class WebHttpApplication : HttpApplication
    {
        /// <summary>
        /// core 启动类
        /// </summary>
        protected CoreBootstrapper Bootstrapper { get; private set; }

        protected WebHttpApplication()
        {
            Bootstrapper = new CoreBootstrapper();
        }

        /// <summary>
        /// application start
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Application_Start(object sender, EventArgs e)
        {
            Bootstrapper.IocManager.Register<IAssemblyFinder, WebAssemblyFinder>();
            Bootstrapper.Initialize();
        }

       
        public virtual void Application_End(object sender, EventArgs e)
        {
            Bootstrapper.Dispose();
        }

       /// <summary>
       /// 会话开始
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        public virtual void Session_Start(object sender, EventArgs e)
        {

        }

       /// <summary>
       /// 会话结束
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        public virtual void Session_End(object sender, EventArgs e)
        {

        }

      /// <summary>
      /// 一个请求进入
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
        public virtual void Application_BeginRequest(object sender, EventArgs e)
        {
           
        }

       /// <summary>
       /// 一个请求结束
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        public virtual void Application_EndRequest(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 身份验证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            TrySetTenantId();
        }
        /// <summary>
        /// 程序出错
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Application_Error(object sender, EventArgs e)
        {

        }

       
        public virtual void TrySetTenantId()
        {
            var claimsPrincipal = User as ClaimsPrincipal;
            if (claimsPrincipal == null)
            {
                return;
            }

            //var claimsIdentity = claimsPrincipal.Identity as ClaimsIdentity;
            //if (claimsIdentity == null)
            //{
            //    return;
            //}

            //var tenantIdClaim = claimsIdentity.Claims.FirstOrDefault(c => c.Type == );
            //if (tenantIdClaim != null)
            //{
            //    return;
            //}

        }

       
    }
}
