using Shared.Features.Messaging.Query;

namespace Modules.Subscriptions.Features.DomainFeatures.StripeSubscriptions.Application.Queries
{
    public class GetSubscriptionForTenant : Query<StripeSubscription>
    {
        public Guid TenantId { get; set; }
    }
}
