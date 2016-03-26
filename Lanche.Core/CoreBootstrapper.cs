using Lanche.Core.Configuration;
using Lanche.Core.Dependency;
using Lanche.Core.Module;
using Lanche.Core.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Lanche.Core
{
    /// <summary>
    /// core 启动类
    /// </summary>
    public class CoreBootstrapper : IDisposable
    {
        //private static object locker = new object();
        public IIocManager IocManager { get; private set; }

        /// <summary>
        /// Is this object disposed before?
        /// </summary>
        protected bool IsDisposed;

        private IModuleManager _moduleManager;

        public void Initialize()
        {
            IocManager.IocContainer.Install(new CoreInstaller());
          
            _moduleManager = IocManager.Resolve<IModuleManager>();
            _moduleManager.InitializeModules();


        }
        public CoreBootstrapper(IIocManager iocManager)
        {
            this.IocManager = iocManager;

        }
        public CoreBootstrapper()
            : this(Lanche.Core.Dependency.IocManager.Instance)
        {

        }
        public void Dispose()
        {

            if (IsDisposed)
            {
                return;
            }

            IsDisposed = true;

            if (_moduleManager != null)
            {
                _moduleManager.ShutdownModules();
            }


        }

    }
}
