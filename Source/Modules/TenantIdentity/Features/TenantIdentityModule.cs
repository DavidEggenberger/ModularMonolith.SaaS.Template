using Modules.TenantIdentity.Features.Infrastructure.Configuration;
using Modules.TenantIdentity.Features.Infrastructure.EFCore;
using Shared.Features.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Modules.TenantIdentity.Features
{
    public class TenantIdentityModule : IModule
    {
        public Assembly FeaturesAssembly => typeof(TenantIdentityModule).Assembly;
        public TenantIdentityConfiguration Configuration { get; set; }
        public TenantIdentityDbContext TenantIdentityDbContext { get; set; }

        public TenantIdentityModule(TenantIdentityConfiguration Configuration, TenantIdentityDbContext DbContext)
        {
            this.TenantIdentityDbContext = DbContext;
            this.Configuration = Configuration;
        }
    }
}
