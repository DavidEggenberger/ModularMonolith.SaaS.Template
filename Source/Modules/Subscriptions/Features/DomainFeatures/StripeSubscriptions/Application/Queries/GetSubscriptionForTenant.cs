using Shared.Features.Messaging.Query;
using Shared.Kernel.BuildingBlocks.Auth.Attributes;

namespace Modules.Subscriptions.Features.DomainFeatures.StripeSubscriptions.Application.Queries
{
    [AuthorizeTenantAdmin]
    public class GetSubscriptionForTenant : Query<StripeSubscription>
    {
        public Guid TenantId { get; set; }
    }
}
