using Microsoft.EntityFrameworkCore;
using Modules.Subscriptions.IntegrationEvents;
using Modules.TenantIdentity.Features.Infrastructure.EFCore;
using Shared.Features.CQRS.IntegrationEvent;
using System.Threading;

namespace Modules.TenantIdentity.Features.Application.IntegrationEvents
{
    public class TenantSubscriptionUpdatedIntegrationEventHandler : IIntegrationEventHandler<TenantSubscriptionPlanUpdatedIntegrationEvent>
    {
        private readonly TenantIdentityDbContext tenantIdentityDbContext;

        public TenantSubscriptionUpdatedIntegrationEventHandler(TenantIdentityDbContext tenantIdentityDbContext)
        {
            this.tenantIdentityDbContext = tenantIdentityDbContext;
        }

        public async Task HandleAsync(TenantSubscriptionPlanUpdatedIntegrationEvent integrationEvent, CancellationToken cancellation)
        {
            var tenant = await tenantIdentityDbContext.Tenants.FirstAsync(tenant => tenant.Id == integrationEvent.TenantId);
            tenant.SubscriptionPlanType = integrationEvent.SubscriptionPlanType;

            await tenantIdentityDbContext.SaveChangesAsync();
        }
    }
}
