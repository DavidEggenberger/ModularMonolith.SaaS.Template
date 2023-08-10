using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.TenantIdentity.DomainFeatures.Infrastructure.EFCore
{
    public class TenantIdentityDbContextFactory : IDesignTimeDbContextFactory<TenantIdentityDbContext>
    {

        public TenantIdentityDbContextFactory()
        { 
        }

        public TenantIdentityDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TenantIdentityDbContext>();
            optionsBuilder.UseSqlServer();

            return new TenantIdentityDbContext(optionsBuilder.Options);
        }
    }
}
