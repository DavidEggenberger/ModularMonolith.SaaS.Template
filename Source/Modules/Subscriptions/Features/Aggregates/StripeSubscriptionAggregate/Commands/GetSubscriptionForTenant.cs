using Shared.Features.CQRS.Query;
using Shared.Kernel.BuildingBlocks.Auth.Attributes;

namespace Modules.Subscriptions.Features.Aggregates.StripeSubscriptionAggregate.Commands
{
    [AuthorizeTenantAdmin]
    public class GetSubscriptionForTenant : IQuery<object>
    {
        public Guid UserId { get; set; }
        public Guid TenantId { get; set; }
    }
}
