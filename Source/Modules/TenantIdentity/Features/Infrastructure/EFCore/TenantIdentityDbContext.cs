using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Shared.Features.Domain.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared.Features.EFCore.Configuration;
using Modules.TenantIdentity.Features.DomainFeatures.UserAggregate;
using Shared.Features.EFCore;
using Shared.Kernel.BuildingBlocks;
using Modules.TenantIdentity.Features.DomainFeatures.TenantAggregate.Domain;
using Modules.TenantIdentity.Features.DomainFeatures.UserAggregate.Infrastructure;

namespace Modules.TenantIdentity.Features.Infrastructure.EFCore
{
    public class TenantIdentityDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        private readonly IServiceProvider serviceProvider;

        public TenantIdentityDbContext()
        {
            
        }
        public TenantIdentityDbContext(IServiceProvider serviceProvider, DbContextOptions<TenantIdentityDbContext> dbContextOptions) : base(dbContextOptions)
        {
            this.serviceProvider = serviceProvider;
        }

        public override DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<TenantInvitation> TenantInvitations { get; set; }
        public DbSet<TenantMembership> TenantMeberships { get; set; }
        public DbSet<TenantConfiguration> TenantConfiguration { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var executionContext = serviceProvider.GetRequiredService<IExecutionContext>();
            var efCoreConfiguration = serviceProvider.GetRequiredService<EFCoreConfiguration>();

            optionsBuilder.AddInterceptors(new ExecutionContextInterceptor());
            optionsBuilder.UseSqlServer(
                executionContext.HostingEnvironment.IsProduction() ? efCoreConfiguration.SQLServerConnectionString_Prod : efCoreConfiguration.SQLServerConnectionString_Dev,
                sqlServerOptions =>
                {
                    sqlServerOptions.CommandTimeout(15);
                    sqlServerOptions.MigrationsHistoryTable($"MigrationHistory_TenantIdentity");
                }
            );

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<ApplicationUser>(new ApplicationUserEFConfiguration());
            modelBuilder.HasDefaultSchema("Identity");
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

        public async Task<ApplicationUser> GetUserByIdAsync(Guid userId)
        {
            var user = await Users.FirstOrDefaultAsync(t => t.Id == userId);
            if (user == null)
            {
                throw new NotFoundException();
            }
            return user;
        }

        public async Task<Tenant> GetTenantByIdAsync(Guid tenantId)
        {
            var tenant = await Tenants.FirstOrDefaultAsync(t => t.TenantId == tenantId);
            if (tenant == null)
            {
                throw new NotFoundException();
            }
            return tenant;
        }

        public async Task<Tenant> GetTenantExtendedByIdAsync(Guid tenantId)
        {
            var tenant = await Tenants
                .Include(t => t.Memberships)
                //.Include(t => t.Invitations)
                .FirstOrDefaultAsync(t => t.TenantId == tenantId);
            if (tenant == null)
            {
                throw new NotFoundException();
            }
            return tenant;
        }
    }
}
