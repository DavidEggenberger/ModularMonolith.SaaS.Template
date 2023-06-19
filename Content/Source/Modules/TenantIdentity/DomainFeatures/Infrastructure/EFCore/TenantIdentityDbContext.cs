using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Modules.TenantIdentity.DomainFeatures.UserAggregate.Domain;
using Modules.TenantIdentity.DomainFeatures.TenantAggregate.Domain;
using Modules.TenantIdentity.DomainFeatures.Infrastructure.EFCore.Configuration;
using Modules.TenantIdentity.DomainFeatures.Infrastructure.EFCore.Configuration.UserAggregate;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Shared.Infrastructure.EFCore;

namespace Modules.TenantIdentity.DomainFeatures.Infrastructure.EFCore
{
    public class TenantIdentityDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        private readonly IConfiguration configuration;
        private readonly IHostEnvironment hostEnvironment;

        public TenantIdentityDbContext(DbContextOptions<TenantIdentityDbContext> dbContextOptions, IConfiguration configuration, IHostEnvironment hostEnvironment) : base(dbContextOptions)
        {
            this.configuration = configuration;
            this.hostEnvironment = hostEnvironment;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (hostEnvironment.IsDevelopment())
            {
                optionsBuilder.UseSqlServer(configuration[EFCoreConfigurationConstants.DevelopmentSQLServerConnectionString]);
            }
            if (hostEnvironment.IsProduction())
            {
                optionsBuilder.UseSqlServer(configuration[EFCoreConfigurationConstants.ProductionSQLServerConnectionString]);
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<User>(new UserConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Tenant> Tenants { get; set; }
    }
}
