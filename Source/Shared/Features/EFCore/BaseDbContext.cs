using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;
using Shared.Features.CQRS.DomainEvent;
using Shared.Features.DomainKernel;
using Shared.Features.EFCore.Configuration;
using Shared.Features.EFCore.MultiTenancy;
using Shared.Features.EFCore.MultiTenancy.Exceptions;
using Shared.Kernel.BuildingBlocks.ContextAccessors;
using Shared.Kernel.Interfaces;

namespace Shared.Features.EFCore
{
    public class BaseDbContext<T> : DbContext where T : DbContext
    {
        private readonly string schemaName;
        private readonly IExecutionContextAccessor executionContextAccessor;
        private readonly EFCoreConfiguration configuration;
        private readonly IDomainEventDispatcher domainEventDispatcher;

        public BaseDbContext(IServiceProvider serviceProvider, string schemaName, DbContextOptions<T> dbContextOptions) : base(dbContextOptions)
        {
            this.schemaName = schemaName;
            domainEventDispatcher = serviceProvider.GetService<IDomainEventDispatcher>();
            executionContextAccessor = serviceProvider.GetService<IExecutionContextAccessor>();
            configuration = serviceProvider.GetService<EFCoreConfiguration>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(schemaName);

            ThrowIfDbSetEntityNotTenantIdentifiable(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(null,
                x => x.Namespace.Contains(typeof(T).Namespace));
            modelBuilder.ApplyBaseEntityConfiguration(executionContextAccessor.TenantId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var hostEnvironment = serviceProvider.GetRequiredService<IHostEnvironment>();

            optionsBuilder.UseSqlServer(configuration.SQLServerConnectionString, sqlServerOptions =>
            {
                sqlServerOptions.EnableRetryOnFailure(5);
            });
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ThrowIfMultipleTenants();
            UpdateAutitableEntities();
            SetTenantId();
            UpdateCreatedByUserEntities(executionContextAccessor.UserId);
            await DispatchEventsAsync(cancellationToken);
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateAutitableEntities()
        {
            foreach (var entry in ChangeTracker.Entries<IAuditable>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastUpdatedAt = DateTime.Now;
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
                entry.Entity.TenantId = executionContextAccessor.TenantId;
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

            if (ids.First() != executionContextAccessor.TenantId)
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

        private async Task DispatchEventsAsync(CancellationToken cancellationToken)
        {
            var domainEntities = ChangeTracker
                .Entries<Entity>()
                .Select(x => x.Entity)
                .Where(x => x.DomainEvents.Any())
                .ToList();

            foreach (var entity in domainEntities)
            {
                var events = entity.DomainEvents.ToArray();
                entity.ClearDomainEvents();
                foreach (var domainEvent in events)
                {
                    await domainEventDispatcher.RaiseAsync(domainEvent, cancellationToken).ConfigureAwait(false);
                }
            }
        }
    }
}
