using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Modules.TenantIdentity.DomainFeatures.UserAggregate.Domain;
using Modules.TenantIdentity.DomainFeatures.TenantAggregate.Domain;

namespace Modules.TenantIdentity.DomainFeatures.Infrastructure.EFCore
{
    public class TenantIdentityDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
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
