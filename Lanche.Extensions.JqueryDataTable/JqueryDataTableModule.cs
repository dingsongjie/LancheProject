using Lanche.Core;
using Lanche.Core.Module;
using Lanche.Domain;
using Lanche.Domain.Repository.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.Extensions.JqueryDataTable
{
    [DependsOn(typeof(CoreModule),typeof(DomainModule))]
    public class JqueryDataTableModule : Module
    {
        public override void PreInitialize()
        {
            IocManager.Register<IPagingRequestEntitySolover, DefaultPagingRequestEntitySolove>();
        }
    }
}
