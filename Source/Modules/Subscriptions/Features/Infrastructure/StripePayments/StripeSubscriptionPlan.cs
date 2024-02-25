using Shared.Kernel.BuildingBlocks.Auth;

namespace Modules.Subscriptions.DomainFeatures.Infrastructure.StripePayments
{
    public class StripeSubscriptionPlan
    {
        public string StripePriceId { get; set; }
        public SubscriptionPlanType Type { get; set; }
    }
}
