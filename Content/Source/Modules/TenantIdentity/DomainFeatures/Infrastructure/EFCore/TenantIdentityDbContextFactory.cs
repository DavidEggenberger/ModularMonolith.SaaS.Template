using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
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
