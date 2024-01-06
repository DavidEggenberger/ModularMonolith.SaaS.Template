using Modules.Subscriptions.IntegrationEvents;
using Shared.Features.CQRS.IntegrationEvent;
using System.Threading;

namespace Modules.TenantIdentity.Features.Aggregates.TenantAggregate.Application.IntegrationEventHandlers
{
    public class TenantSubscriptionCreatedIntegrationEventHandler : IIntegrationEventHandler<TenantSubscriptionCreatedIntegrationEvent>
    {
        public Task HandleAsync(TenantSubscriptionCreatedIntegrationEvent integrationEvent, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }
    }
}
