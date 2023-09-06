using Modules.TenantIdentity.IntegrationEvents;
using Shared.Infrastructure.CQRS.IntegrationEvent;

namespace Modules.Subscription.DomainFeatures.Aggregates.StripeSubscriptionAggregate.Application.IntegrationEventHandlers
{
    public class UserUpdatedIntegrationEventHandler : IIntegrationEventHandler<UserEmailUpdatedIntegrationEvent>
    {
        public async Task HandleAsync(UserEmailUpdatedIntegrationEvent userEmailUpdatedIntegrationEvent, CancellationToken cancellationToken)
        {

        }
    }
}
