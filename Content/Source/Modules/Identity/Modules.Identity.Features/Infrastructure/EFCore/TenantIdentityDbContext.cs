using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Modules.IdentityModule.Domain;
using Modules.TenantIdentity.Features.UserAggregate.Domain;
using Modules.TenantIdentity.Features.TenantAggregate.Domain;

namespace Modules.TenantIdentityModule.Infrastructure.EFCore
{
    public class TenantIdentityDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        private readonly IConfiguration configuration;
        public TenantIdentityDbContext(IConfiguration configuration, DbContextOptions<IdentityDbContext> options) : base(options)
        {
            this.configuration = configuration;
        }

        //public TenantIdentityDbContext(DbContextOptions<TenantIdentityDbContext> dbContextOptions, IServiceProvider serviceProvider, IConfiguration configuration) : base(dbContextOptions, serviceProvider, configuration)
        //{

        //}
        public DbSet<Tenant> Tenants { get; set; }
    }
}
