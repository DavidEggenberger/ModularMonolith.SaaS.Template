using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Modules.TenantIdentity.DomainFeatures.UserAggregate.Domain;

namespace Modules.TenantIdentity.DomainFeatures.Infrastructure.EFCore
{
    public class IdentityDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        private readonly IConfiguration configuration;
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(optionsBuilder.IsConfigured is false)
            {
                optionsBuilder.UseInMemoryDatabase("InMemoryDb");
                //optionsBuilder.UseSqlServer(configuration.GetConnectionString("IdentityDbLocalConnectionString"), sqlServerOptions =>
                //{
                //    //sqlServerOptions.MigrationsAssembly(typeof(IAssemblyMarker).GetTypeInfo().Assembly.GetName().Name);
                //    sqlServerOptions.EnableRetryOnFailure(5);
                //});
            }   
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<User>(new UserConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
