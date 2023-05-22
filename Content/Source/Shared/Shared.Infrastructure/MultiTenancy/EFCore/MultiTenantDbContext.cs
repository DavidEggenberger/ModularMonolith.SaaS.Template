using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Shared.SharedKernel.Interfaces;
using Shared.Domain.Attributes;
using SharedKernel.Interfaces;
using Shared.Infrastructure.MultiTenancy.Services;
using Shared.Infrastructure.MultiTenancy.Exceptions;

namespace Shared.Infrastructure.MultiTenancy.EFCore
{
    public class MultiTenantDbContext<T> : DbContext where T : DbContext
    {
        private readonly ITenantResolver tenantResolver;
        private readonly IExecutionContextAccessor userResolver;
        private readonly Guid tenantId;
        private readonly IConfiguration configuration;
        public MultiTenantDbContext(DbContextOptions<T> dbContextOptions, IServiceProvider serviceProvider, IConfiguration configuration) : base(dbContextOptions)
        {
            tenantResolver = serviceProvider.GetRequiredService<ITenantResolver>();
            userResolver = serviceProvider.GetRequiredService<IExecutionContextAccessor>();
            tenantId = tenantResolver.CanResolveTenant() is true ? tenantResolver.ResolveTenantId() : Guid.NewGuid();// Ensure Guid for EF Core Migrations
            this.configuration = configuration;
        }

        protected sealed override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(optionsBuilder.IsConfigured is false)
            {
                //optionsBuilder.UseSqlServer(configuration.GetConnectionString("ApplicationDbContextConnection"), options =>
                //{
                //    options.MigrationsAssembly(typeof(IAssemblyMarker).GetTypeInfo().Assembly.GetName().Name);
                //    options.EnableRetryOnFailure(5);
                //    options.MigrationsHistoryTable("EFCore_MigrationHistory");
                //});
            }
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
            foreach (var entityType in modelBuilder.Model.GetEntityTypes().Where(t => t.ClrType.GetCustomAttribute<AggregateRootAttribute>() is not null))
            {
                if(typeof(ITenantIdentifiable).IsAssignableFrom(entityType.ClrType) is false)
                {
                    throw new EntityNotTenantIdentifiableException(entityType.ClrType.Name);
                }
            }
        }
    }
}
