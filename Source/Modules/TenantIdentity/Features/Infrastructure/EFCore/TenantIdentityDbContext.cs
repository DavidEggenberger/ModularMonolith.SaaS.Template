using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Shared.Features.Domain.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared.Features.EFCore.Configuration;
using Modules.TenantIdentity.Features.Aggregates.UserAggregate;
using Modules.TenantIdentity.Features.Domain.TenantAggregate;
using Modules.TenantIdentity.Features.Infrastructure.EFCore.Configuration;

namespace Modules.TenantIdentity.Features.Infrastructure.EFCore
{
    public class TenantIdentityDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        private readonly IServiceProvider serviceProvider;
        private readonly EFCoreConfiguration configuration;

        public TenantIdentityDbContext()
        {
            
        }
        public TenantIdentityDbContext(IServiceProvider serviceProvider, DbContextOptions<TenantIdentityDbContext> dbContextOptions) : base(dbContextOptions)
        {
            this.serviceProvider = serviceProvider;
            configuration = serviceProvider.GetService<EFCoreConfiguration>();
        }

        public override DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<TenantInvitation> TenantInvitations { get; set; }
        public DbSet<TenantMembership> TenantMeberships { get; set; }
        public DbSet<TenantSettings> TenantSettings { get; set; }
        public DbSet<TenantStyling> TenantStylings { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var hostEnvironment = serviceProvider.GetRequiredService<IHostEnvironment>();

            if (hostEnvironment.IsDevelopment())
            {
                optionsBuilder.UseSqlServer(configuration.SQLServerConnectionString_Dev, sqlServerOptions =>
                {
                    sqlServerOptions.EnableRetryOnFailure(5);
                });
            }
            if (hostEnvironment.IsProduction())
            {
                optionsBuilder.UseSqlServer(configuration.SQLServerConnectionString_Prod, sqlServerOptions =>
                {
                    sqlServerOptions.EnableRetryOnFailure(5);
                });
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<ApplicationUser>(new UserConfiguration());
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
                .Include(t => t.Invitations)
                .FirstOrDefaultAsync(t => t.TenantId == tenantId);
            if (tenant == null)
            {
                throw new NotFoundException();
            }
            return tenant;
        }
    }
}
