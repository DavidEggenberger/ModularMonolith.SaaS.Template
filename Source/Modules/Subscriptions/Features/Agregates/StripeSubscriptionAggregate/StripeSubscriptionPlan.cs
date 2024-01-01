using Shared.Kernel.BuildingBlocks.Auth;

namespace Modules.Subscriptions.Features.Agregates.SubscriptionAggregate
{
    public class StripeSubscriptionPlan
    {
        public string StripePriceId { get; set; }
        public SubscriptionPlanType Type { get; set; }
    }
}
