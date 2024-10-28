using Modules.Subscriptions.Features.DomainFeatures.StripeCustomers;
using Shared.Features.Domain;
using Shared.Kernel.BuildingBlocks.Auth;

namespace Modules.Subscriptions.Features.DomainFeatures.StripeSubscriptions
{
    public class StripeSubscription : Entity
    {
        private StripeSubscription() { }

        public string StripePortalSubscriptionId { get; set; }
        public Guid StripeCustomerId { get; set; }
        public StripeCustomer StripeCustomer { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public SubscriptionPlanType PlanType { get; set; }
        public StripeSubscriptionStatus Status { get; set; }

        public static StripeSubscription Create(
            DateTime? expirationDate,
            string stripePortalSubscriptionId,
            SubscriptionPlanType subscriptionPlanType,
            StripeSubscriptionStatus stripeSubscriptionStatus,
            Guid tenantId,
            StripeCustomer stripeCustomer)
        {
            return new StripeSubscription()
            {
                ExpirationDate = expirationDate,
                StripePortalSubscriptionId = stripePortalSubscriptionId,
                PlanType = subscriptionPlanType,
                Status = stripeSubscriptionStatus,
                TenantId = tenantId,
                StripeCustomer = stripeCustomer
            };
        }
    }
}
