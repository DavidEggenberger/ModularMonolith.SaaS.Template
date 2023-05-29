using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.DomainFeatures.Infrastructure.MultiTenancy.EFCore;
using Shared.DomainFeatures.Infrastructure.CQRS.DomainEvent;
using Shared.Kernel.BuildingBlocks.Authorization.Services;

namespace Shared.DomainFeatures.Infrastructure.EFCore
{
    public class BaseDbContext<T> : MultiTenantDbContext<T> where T : DbContext
    {
        private readonly IDomainEventDispatcher domainEventDispatcher;
        public BaseDbContext(DbContextOptions<T> dbContextOptions, IServiceProvider serviceProvider, IConfiguration configuration) : base(dbContextOptions, serviceProvider, configuration)
        {
            this.domainEventDispatcher = serviceProvider.GetRequiredService<IDomainEventDispatcher>();
        }

        public IUserAuthorizationService TenantAuthorizationService { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var changedRowCount = await base.SaveChangesAsync(cancellationToken);
            await DispatchEventsAsync(cancellationToken);
            return changedRowCount;
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
