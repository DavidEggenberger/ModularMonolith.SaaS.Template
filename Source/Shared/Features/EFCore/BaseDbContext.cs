using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared.Features.DomainKernel;
using Shared.Features.EFCore.Configuration;
using Shared.Features.EFCore.MultiTenancy;
using Shared.Features.EFCore.MultiTenancy.Exceptions;
using Shared.Kernel.BuildingBlocks.ExecutionContext;
using Shared.Kernel.Interfaces;

namespace Shared.Features.EFCore
{
    public class BaseDbContext<T> : DbContext where T : DbContext
    {
        private readonly string schemaName;
        private readonly IExecutionContext executionContext;
        private readonly EFCoreConfiguration configuration;

        public BaseDbContext(IServiceProvider serviceProvider, string schemaName, DbContextOptions<T> dbContextOptions) : base(dbContextOptions)
        {
            this.schemaName = schemaName;
            executionContext = serviceProvider.GetService<IExecutionContext>();
            configuration = serviceProvider.GetService<EFCoreConfiguration>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(schemaName);

            ThrowIfDbSetEntityNotTenantIdentifiable(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(null,
                x => x.Namespace.Contains(typeof(T).Namespace));
            modelBuilder.ApplyBaseEntityConfiguration(executionContext.TenantId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(new ExecutionContextInterceptor());
        
            if (executionContext.HostingEnvironment.IsDevelopment())
            {
                optionsBuilder.UseSqlServer(configuration.SQLServerConnectionString_Dev, sqlServerOptions =>
                {
                    sqlServerOptions.EnableRetryOnFailure(5);
                });
            }
            if (executionContext.HostingEnvironment.IsProduction())
            {
                optionsBuilder.UseSqlServer(configuration.SQLServerConnectionString_Prod, sqlServerOptions =>
                {
                    sqlServerOptions.EnableRetryOnFailure(5);
                });
            }

            base.OnConfiguring(optionsBuilder);     
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ThrowIfMultipleTenants();
            UpdateAutitableEntities();
            SetTenantId();
            UpdateCreatedByUserEntities(executionContext.UserId);
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
                entry.Entity.TenantId = executionContext.TenantId;
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

            if (ids.First() != executionContext.TenantId)
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
