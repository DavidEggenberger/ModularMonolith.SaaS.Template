using Modules.TenantIdentity.IntegrationEvents;
using Shared.Infrastructure.CQRS.IntegrationEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Subscription.DomainFeatures.Aggregates.StripeSubscriptionAggregate.Application.IntegrationEventHandlers
{
    public class UserCreatedIntegrationEventHandler : IIntegrationEventHandler<UserCreatedIntegrationEvent>
    {
        public async Task HandleAsync(UserCreatedIntegrationEvent userCreatedIntegrationEvent, CancellationToken cancellation)
        {

        }
    }
}
