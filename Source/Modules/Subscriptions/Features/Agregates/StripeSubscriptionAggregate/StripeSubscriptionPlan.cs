using Shared.Kernel.BuildingBlocks.Authorization;

namespace Modules.Subscriptions.Features.Agregates.SubscriptionAggregate
{
    public class StripeSubscriptionPlan
    {
        public string StripePriceId { get; set; }
        public SubscriptionPlanType Type { get; set; }
    }
}
