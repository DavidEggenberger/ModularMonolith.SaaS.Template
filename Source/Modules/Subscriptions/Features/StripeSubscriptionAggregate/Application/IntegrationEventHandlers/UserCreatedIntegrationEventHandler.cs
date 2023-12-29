using Modules.TenantIdentity.IntegrationEvents;
using Shared.Features.CQRS.IntegrationEvent;

namespace Modules.Subscriptions.Features.StripeSubscriptionAggregate.Application.IntegrationEventHandlers
{
    public class UserCreatedIntegrationEventHandler : IIntegrationEventHandler<UserCreatedIntegrationEvent>
    {
        public async Task HandleAsync(UserCreatedIntegrationEvent userCreatedIntegrationEvent, CancellationToken cancellation)
        {

        }
    }
}
