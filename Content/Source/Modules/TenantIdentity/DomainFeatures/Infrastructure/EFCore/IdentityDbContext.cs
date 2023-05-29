using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Modules.TenantIdentity.DomainFeatures.UserAggregate.Domain;

namespace Modules.TenantIdentity.DomainFeatures.Infrastructure.EFCore
{
    public class IdentityDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        private readonly IConfiguration configuration;
        public IdentityDbContext(IConfiguration configuration, DbContextOptions<IdentityDbContext> options) : base(options)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(optionsBuilder.IsConfigured is false)
            {
                //optionsBuilder.UseSqlServer(configuration.GetConnectionString("IdentityDbLocalConnectionString"), sqlServerOptions =>
                //{
                //    //sqlServerOptions.MigrationsAssembly(typeof(IAssemblyMarker).GetTypeInfo().Assembly.GetName().Name);
                //    sqlServerOptions.EnableRetryOnFailure(5);
                //});
            }   
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<ApplicationUser>(new ApplicationUserConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
