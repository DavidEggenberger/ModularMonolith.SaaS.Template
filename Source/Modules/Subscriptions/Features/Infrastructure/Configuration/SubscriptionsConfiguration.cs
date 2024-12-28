using Modules.Subscriptions.Features.Infrastructure.StripePayments;
using Shared.Features.Configuration;
using Shared.Kernel.DomainKernel;

namespace Modules.Subscriptions.Features.Infrastructure.Configuration
{
    public class SubscriptionsConfiguration : ConfigurationObject
    {
        public string StripeAPIKey { get; set; }
        public string StripeEndpointSecret { get; set; }
        public string StripeProfessionalPlanPriceId { get; set; }
        public string StripeEnterprisePlanPriceId { get; set; }

        public StripeSubscriptionPlan GetSubscriptionType(SubscriptionPlanType subscriptionPlanType)
        {
            return Subscriptions.Single(s => s.Type == subscriptionPlanType);
        }

        public List<StripeSubscriptionPlan> Subscriptions => new List<StripeSubscriptionPlan>()
        {
            new StripeSubscriptionPlan
            {
                Type = SubscriptionPlanType.Professional,
                TrialPeriodDays = 14,
                StripePriceId = StripeProfessionalPlanPriceId
            },
            new StripeSubscriptionPlan
            {
                Type = SubscriptionPlanType.Enterprise,
                TrialPeriodDays = 14,
                StripePriceId = StripeEnterprisePlanPriceId
            }
        };
    }
}
