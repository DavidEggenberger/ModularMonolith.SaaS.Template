using Modules.Subscriptions.Features.Infrastructure.StripePayments;
using Shared.Features.Modules.Configuration;
using Shared.Kernel.BuildingBlocks.Auth;

namespace Modules.Subscription.Features.Infrastructure.Configuration
{
    public class SubscriptionsConfiguration : IModuleConfiguration
    {
        public string StripeAPIKey { get; set; }
        public string StripeEndpointSecret { get; set; }
        public string StripeProfessionalPlanPriceId { get; set; }

        public StripeSubscriptionType GetSubscriptionType(SubscriptionPlanType subscriptionPlanType)
        {
            return Subscriptions.Single(s => s.Type == subscriptionPlanType);
        }

        public List<StripeSubscriptionType> Subscriptions => new List<StripeSubscriptionType>()
        {
            new StripeSubscriptionType
            {
                Type = SubscriptionPlanType.Professional,
                TrialPeriodDays = 14,
                StripePriceId = StripeProfessionalPlanPriceId
            }
        };
    }
}
