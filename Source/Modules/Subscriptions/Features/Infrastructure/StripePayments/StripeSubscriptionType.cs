using Shared.Kernel.BuildingBlocks.Authorization;

namespace Modules.Subscriptions.Features.Infrastructure.StripePayments
{
    public class StripeSubscriptionType
    {
        public string StripePriceId { get; set; }
        public SubscriptionPlanType Type { get; set; }
        public int TrialPeriodDays { get; set; }
    }
}
