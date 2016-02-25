using Castle.Core;
using Castle.MicroKernel;
using Lanche.Core.Application;
using Lanche.Core.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.MessageQueue.WorkSpace
{
    class MqChannelWorkerRegister
    {
        /// <summary>
        /// 拦截器注册
        /// </summary>
       

            public static void Initialize(IIocManager iocManager)
            {
                iocManager.IocContainer.Kernel.ComponentRegistered += ComponentRegistered;
            }

            private static void ComponentRegistered(string key, IHandler handler)
            {
                if (typeof(IApplicationBiz).IsAssignableFrom(handler.ComponentModel.Implementation))
                {

                    handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(MqChannelWorkerInterceptor)));
                }
               
            }
        }
    
}
