using Lanche.Core.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.Core.Configuration
{
    public class DefaultConfigurationManager : IConfigurationManager
    {
        private readonly IIocManager _iocManager;
        public DefaultConfigurationManager(IIocManager iocManager)
        {
            _iocManager = iocManager;
        }


        public IStartupConfiguration Get<TConfigType>() where TConfigType : IStartupConfiguration
        {
            return _iocManager.Resolve<TConfigType>();
        }

        public IStartupConfiguration Get(Type configurationType)
        {
            return (IStartupConfiguration)_iocManager.Resolve(configurationType);
        }



        public void Replace<TConfigType, TConfigImpl>()
            where TConfigType : class, IStartupConfiguration
            where TConfigImpl : class, TConfigType
        {
            _iocManager.Replace<TConfigType, TConfigImpl>();
        }

        public void Replace(Type configurationType, Type configurationImpl)
        {
            _iocManager.Replace(configurationType, configurationImpl);
        }

        public void Add<TIStartConfigu, TIStartConfiguImpl>()
            where TIStartConfigu : class, IStartupConfiguration
            where TIStartConfiguImpl : class, TIStartConfigu
        {
            _iocManager.Register<TIStartConfigu, TIStartConfiguImpl>();
        }

        public void Add(Type configurationType, Type configurationImpl)
        {
            _iocManager.Register(configurationType, configurationImpl);
        }




       
    }
}
