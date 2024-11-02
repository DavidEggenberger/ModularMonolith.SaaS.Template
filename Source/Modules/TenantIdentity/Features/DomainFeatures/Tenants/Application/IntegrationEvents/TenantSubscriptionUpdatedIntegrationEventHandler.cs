using Microsoft.EntityFrameworkCore;
using Modules.Subscriptions.IntegrationEvents;
using Shared.Features.Messaging.IntegrationEvents;
using Shared.Features.Server;
using System.Threading;

namespace Modules.TenantIdentity.Features.DomainFeatures.Tenants.Application.IntegrationEvents
{
    public class TenantSubscriptionUpdatedIntegrationEventHandler : ServerExecutionBase<TenantIdentityModule>, IIntegrationEventHandler<TenantSubscriptionPlanUpdatedIntegrationEvent>
    {
        public TenantSubscriptionUpdatedIntegrationEventHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }

        public async Task HandleAsync(TenantSubscriptionPlanUpdatedIntegrationEvent integrationEvent, CancellationToken cancellation)
        {
            var tenant = await module.TenantIdentityDbContext.Tenants.FirstAsync(tenant => tenant.Id == integrationEvent.TenantId);
            tenant.SubscriptionPlanType = integrationEvent.SubscriptionPlanType;

            await module.TenantIdentityDbContext.SaveChangesAsync();
        }
    }
}
