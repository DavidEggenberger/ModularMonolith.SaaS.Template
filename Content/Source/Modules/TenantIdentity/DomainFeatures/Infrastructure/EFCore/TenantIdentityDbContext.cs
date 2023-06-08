using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Modules.TenantIdentity.DomainFeatures.UserAggregate.Domain;
using Modules.TenantIdentity.DomainFeatures.TenantAggregate.Domain;
using Modules.TenantIdentity.DomainFeatures.Infrastructure.EFCore.Configuration;
using Modules.TenantIdentity.DomainFeatures.Infrastructure.EFCore.Configuration.UserAggregate;

namespace Modules.TenantIdentity.DomainFeatures.Infrastructure.EFCore
{
    public class TenantIdentityDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {

        public TenantIdentityDbContext(DbContextOptions<TenantIdentityDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseInMemoryDatabase("asdfasdf");
            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<User>(new UserConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Tenant> Tenants { get; set; }
    }
}
