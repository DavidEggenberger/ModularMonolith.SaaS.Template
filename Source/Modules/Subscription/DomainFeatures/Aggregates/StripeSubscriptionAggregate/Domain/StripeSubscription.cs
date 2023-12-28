using Shared.Kernel.BuildingBlocks.Authorization;

namespace Modules.Subscription.Features.Aggregates.StripeSubscriptionAggregate.Domain
{
    public class StripeSubscription
    {
        public Guid TenantId { get; set; }
        public StripeCustomer StripeCustomer { get; set; }
        public DateTime ExpirationDate { get; set; }
        public SubscriptionPlanType PlanType { get; set; }
        public StripeSubscriptionStatus Status { get; set; }
    }
}
