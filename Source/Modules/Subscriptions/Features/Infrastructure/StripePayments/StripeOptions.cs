using Shared.Kernel.BuildingBlocks.Auth;

namespace Modules.Subscriptions.Features.Infrastructure.StripePayments
{
    public class StripeOptions
    {
        public string EndpointSecret { get; set; }
        public string ProfessionalPlanPriceId { get; set; }
        public string EnterprisePlanPriceId { get; set; }

        public List<StripeSubscriptionPlan> GetStripeSubscriptionPlans()
        {
            return new List<StripeSubscriptionPlan>()
            {
                new StripeSubscriptionPlan
                {
                    Type = SubscriptionPlanType.Professional,
                    StripePriceId = ProfessionalPlanPriceId
                },
                new StripeSubscriptionPlan
                {
                    Type = SubscriptionPlanType.Enterprise,
                    StripePriceId = EnterprisePlanPriceId
                }
            };
        }
    }
}
