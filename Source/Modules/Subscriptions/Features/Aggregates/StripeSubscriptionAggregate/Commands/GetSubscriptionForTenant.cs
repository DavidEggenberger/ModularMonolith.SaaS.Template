using Shared.Features.CQRS.Query;

namespace Modules.Subscriptions.Features.Aggregates.StripeSubscriptionAggregate.Commands
{
    public class GetSubscriptionForTenant : IQuery<object>
    {
        public Guid UserId { get; set; }
        public Guid TenantId { get; set; }
    }
}
