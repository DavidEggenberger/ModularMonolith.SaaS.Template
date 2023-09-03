using Modules.Subscription.IntegrationEvents;
using Shared.Infrastructure.CQRS.IntegrationEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
