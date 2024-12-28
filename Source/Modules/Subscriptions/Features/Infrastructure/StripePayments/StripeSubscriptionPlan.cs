using Shared.Kernel.DomainKernel;

namespace Modules.Subscriptions.Features.Infrastructure.StripePayments
{
    public class StripeSubscriptionPlan
    {
        public string StripePriceId { get; set; }
        public SubscriptionPlanType Type { get; set; }
        public int TrialPeriodDays { get; set; }
    }
}
