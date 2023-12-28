namespace Modules.Subscription.Features.Infrastructure.Configuration
{
    public class SubscriptionConfiguration
    {
        public string StripeProfessionalPlanId { get; set; }
        public string StripeEnterprisePlanId { get; set; }
        public const string StripeAPIKeyConstant = "Subscription:StripeAPIKey";
    }
}
