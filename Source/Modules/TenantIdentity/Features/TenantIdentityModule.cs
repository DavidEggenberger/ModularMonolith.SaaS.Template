using Modules.TenantIdentity.Features.Infrastructure.Configuration;
using Modules.TenantIdentity.Features.Infrastructure.EFCore;
using Shared.Features.Modules;
using System.Reflection;

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
