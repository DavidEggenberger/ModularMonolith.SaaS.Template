using Microsoft.EntityFrameworkCore;
using Modules.Subscriptions.Public.IntegrationMessages;
using Shared.Features.Messaging.IntegrationMessages;
using Shared.Features.Misc.ExecutionContext;
using System.Threading;

namespace Modules.TenantIdentity.Features.DomainFeatures.Tenants.Application.IntegrationMessageHandlers
{
    public class TenantSubscriptionUpdatedIntegrationEventHandler : ServerExecutionBase<TenantIdentityModule>, IIntegrationEventHandler<TenantSubscriptionPlanUpdatedEvent>
    {
        public TenantSubscriptionUpdatedIntegrationEventHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }

        public async Task HandleAsync(TenantSubscriptionPlanUpdatedEvent integrationEvent, CancellationToken cancellation)
        {
            var tenant = await module.TenantIdentityDbContext.Tenants.FirstAsync(tenant => tenant.Id == integrationEvent.TenantId);
            tenant.UpdateSubscriptionPlan(integrationEvent.SubscriptionPlanType);

            await module.TenantIdentityDbContext.SaveChangesAsync();
        }
    }
}
