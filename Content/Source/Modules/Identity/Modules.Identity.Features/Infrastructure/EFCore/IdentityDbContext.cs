using Shared.Modules.Layers.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Modules;
using Modules.IdentityModule.Domain;
using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Modules.TenantIdentity.Features.UserAggregate.Domain;

namespace Modules.TenantIdentityModule.Infrastructure.EFCore
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
