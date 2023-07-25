using Modules.TenantIdentity.IntegrationEvents;
using Shared.Infrastructure.CQRS.Command;
using Shared.Infrastructure.CQRS.IntegrationEvent;

namespace Modules.Subscription.DomainFeatures.StripeSubscriptionAggregate.Application.IntegrationEvents
{
    public class UserUpdatedIntegrationEventHandler : IIntegrationEventHandler<UserEmailUpdatedIntegrationEvent>
    {
        public async Task HandleAsync(UserEmailUpdatedIntegrationEvent userEmailUpdatedIntegrationEvent, CancellationToken cancellationToken)
        {

        }
    }
}
