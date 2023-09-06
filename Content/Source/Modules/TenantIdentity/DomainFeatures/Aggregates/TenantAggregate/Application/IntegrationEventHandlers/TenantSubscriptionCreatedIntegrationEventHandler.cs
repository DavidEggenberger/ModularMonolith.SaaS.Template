using Modules.Subscription.IntegrationEvents;
using Shared.Infrastructure.CQRS.IntegrationEvent;
using System.Threading;

namespace Modules.TenantIdentity.DomainFeatures.Aggregates.TenantAggregate.Application.IntegrationEventHandlers
{
    public class TenantSubscriptionCreatedIntegrationEventHandler : IIntegrationEventHandler<TenantSubscriptionCreatedIntegrationEvent>
    {
        public Task HandleAsync(TenantSubscriptionCreatedIntegrationEvent integrationEvent, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }
    }
}
