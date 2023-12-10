using Modules.TenantIdentity.IntegrationEvents;
using Shared.DomainFeatures.CQRS.IntegrationEvent;

namespace Modules.Subscription.DomainFeatures.Aggregates.StripeSubscriptionAggregate.Application.IntegrationEventHandlers
{
    public class UserCreatedIntegrationEventHandler : IIntegrationEventHandler<UserCreatedIntegrationEvent>
    {
        public async Task HandleAsync(UserCreatedIntegrationEvent userCreatedIntegrationEvent, CancellationToken cancellation)
        {

        }
    }
}
