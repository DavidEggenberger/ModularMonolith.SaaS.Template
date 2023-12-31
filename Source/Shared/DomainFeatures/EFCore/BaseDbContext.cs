using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.Features.CQRS.DomainEvent;
using Shared.Features.DomainKernel;
using Shared.Features.EFCore.MultiTenancy;

namespace Shared.Features.EFCore
{
    public class BaseDbContext<T> : MultiTenantDbContext<T> where T : DbContext
    {
        private readonly IDomainEventDispatcher domainEventDispatcher;
        public BaseDbContext(IServiceProvider serviceProvider, DbContextOptions<T> dbContextOptions) : base(dbContextOptions)
        {
            domainEventDispatcher = serviceProvider.GetService<IDomainEventDispatcher>();
        }

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
