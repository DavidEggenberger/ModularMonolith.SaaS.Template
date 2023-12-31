using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Shared.Features.MultiTenancy.Services;
using Shared.Features.MultiTenancy.Exceptions;
using Shared.Kernel.BuildingBlocks;
using Shared.Kernel.Interfaces;
using Shared.Features.DomainKernel.Attributes;
using Microsoft.Extensions.Hosting;
using Shared.Features.EFCore.Configuration;
using Microsoft.AspNetCore.Authorization;
using Shared.Features.DomainKernel;

namespace Shared.Features.EFCore.MultiTenancy
{
    public class MultiTenantDbContext<T> : DbContext where T : DbContext
    {
        protected readonly ITenantResolver tenantResolver;
        protected readonly IExecutionContextAccessor userResolver;
        protected readonly Guid tenantId;
        protected readonly EFCoreConfiguration configuration;
        protected readonly IServiceProvider serviceProvider;

        public MultiTenantDbContext(DbContextOptions<T> dbContextOptions) : base(dbContextOptions)
        {
            tenantResolver = serviceProvider.GetRequiredService<ITenantResolver>();
            userResolver = serviceProvider.GetRequiredService<IExecutionContextAccessor>();
            tenantId = tenantResolver.CanResolveTenant() is true ? tenantResolver.ResolveTenantId() : Guid.NewGuid();// Ensure Guid for EF Core Migrations
            configuration = serviceProvider.GetRequiredService<EFCoreConfiguration>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var hostEnvironment = serviceProvider.GetRequiredService<IHostEnvironment>();

            optionsBuilder.UseSqlServer(configuration.SQLServerConnectionString, sqlServerOptions =>
            {
                sqlServerOptions.EnableRetryOnFailure(5);
            });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ThrowIfDbSetEntityNotTenantIdentifiable(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(null,
                x => x.Namespace.Contains(typeof(T).Namespace));
            modelBuilder.ApplyBaseEntityConfiguration(tenantId);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ThrowIfMultipleTenants();
            UpdateAutitableEntities();
            SetTenantId();
            UpdateCreatedByUserEntities(userResolver.UserId);
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateAutitableEntities()
        {
            foreach (var entry in ChangeTracker.Entries<IAuditable>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastUpdated = DateTime.Now;
                        break;
                }
            }
        }

        private void UpdateCreatedByUserEntities(Guid userId)
        {
            foreach (var entry in ChangeTracker.Entries<IAuditable>().Where(x => x.State == EntityState.Added))
            {
                entry.Entity.CreatedByUserId = userId;
            }
        }

        private void SetTenantId()
        {
            foreach (var entry in ChangeTracker.Entries<ITenantIdentifiable>().Where(x => x.State == EntityState.Added))
            {
                entry.Entity.TenantId = tenantId;
            }
        }

        private void ThrowIfMultipleTenants()
        {
            var ids = ChangeTracker.Entries()
                    .Where(e => e.Entity is ITenantIdentifiable)
                    .Select(e => (e.Entity as ITenantIdentifiable).TenantId)
                    .Distinct()
                    .ToList();

            if (ids.Count == 0)
            {
                return;
            }

            if (ids.Count > 1)
            {
                throw new CrossTenantUpdateException(ids);
            }

            if (ids.First() != tenantId)
            {
                throw new CrossTenantUpdateException(ids);
            }
        }

        private void ThrowIfDbSetEntityNotTenantIdentifiable(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes().Where(t => t is AggregateRoot))
            {
                if (typeof(ITenantIdentifiable).IsAssignableFrom(entityType.ClrType) is false)
                {
                    throw new EntityNotTenantIdentifiableException(entityType.ClrType.Name);
                }
            }
        }
    }
}
