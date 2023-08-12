using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Shared.Infrastructure.EFCore.Configuration;

namespace Modules.TenantIdentity.DomainFeatures.Infrastructure.EFCore
{
    public class TenantIdentityDbContextFactory : IDesignTimeDbContextFactory<TenantIdentityDbContext>
    {
        private readonly IHostEnvironment hostEnvironment;
        private readonly IOptions<EFCoreConfiguration> configuration;

        public TenantIdentityDbContextFactory()
        { 
        }

        public TenantIdentityDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TenantIdentityDbContext>();

            if (hostEnvironment.IsDevelopment())
            {
                optionsBuilder.UseSqlServer(configuration.Value.DevelopmentSQLServerConnectionString);
            }
            if (hostEnvironment.IsProduction())
            {
                optionsBuilder.UseSqlServer(configuration.Value.ProductionSQLServerConnectionString);
            }

            return new TenantIdentityDbContext(optionsBuilder.Options);
        }
    }
}
