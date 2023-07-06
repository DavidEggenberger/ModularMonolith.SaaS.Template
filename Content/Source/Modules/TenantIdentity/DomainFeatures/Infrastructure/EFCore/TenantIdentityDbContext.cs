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
using Shared.Kernel.BuildingBlocks.Authorization;

namespace Modules.TenantIdentity.DomainFeatures.Infrastructure.EFCore
{
    public class TenantIdentityDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        private readonly IConfiguration configuration;
        private readonly IHostEnvironment hostEnvironment;
        public readonly IAuthorizationService AuthorizationService;

        public TenantIdentityDbContext(DbContextOptions<TenantIdentityDbContext> dbContextOptions, IConfiguration configuration, IHostEnvironment hostEnvironment, IAuthorizationService authorizationService) : base(dbContextOptions)
        {
            this.configuration = configuration;
            this.hostEnvironment = hostEnvironment;
            this.AuthorizationService = authorizationService;
        }

        public override DbSet<User> Users { get; set; }
        public DbSet<UserSettings> UserSettings { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<TenantInvitation> TenantInvitations { get; set; }
        public DbSet<TenantMembership> TenantMeberships { get; set; }
        public DbSet<TenantSettings> TenantSettings { get; set; }
        public DbSet<TenantStyling> TenantStylings { get; set; }
        public DbSet<TenantSubscription> TenantSubscriptions { get; set; }


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

        public async Task<IEnumerable<Tenant>> GetAllTenantsForUser(Guid userId)
        {
            return await Users.Where(u => u.Id == userId)
                .Include(t => t.TenantMemberships)
                .ThenInclude(tm => tm.Tenant)
                .SelectMany(u => u.TenantMemberships.Select(tm => tm.Tenant))
                .ToListAsync();
        }
    }
}
