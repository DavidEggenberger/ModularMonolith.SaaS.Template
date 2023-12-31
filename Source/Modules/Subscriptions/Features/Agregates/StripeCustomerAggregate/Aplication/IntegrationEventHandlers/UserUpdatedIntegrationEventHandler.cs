using Modules.TenantIdentity.IntegrationEvents;
using Shared.Features.CQRS.IntegrationEvent;

namespace Modules.Subscriptions.Features.Agregates.StripeCustomerAggregate.Aplication.IntegrationEventHandlers
{
    public class UserUpdatedIntegrationEventHandler : IIntegrationEventHandler<UserEmailUpdatedIntegrationEvent>
    {
        public async Task HandleAsync(UserEmailUpdatedIntegrationEvent userEmailUpdatedIntegrationEvent, CancellationToken cancellationToken)
        {

        }
    }
}
