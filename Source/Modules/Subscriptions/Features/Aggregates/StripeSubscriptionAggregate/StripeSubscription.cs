using Modules.Subscriptions.Features.Agregates.StripeCustomerAggregate;
using Shared.Features.Domain;
using Shared.Kernel.BuildingBlocks.Auth;

namespace Modules.Subscriptions.Features.Agregates.StripeSubscriptionAggregate
{
    public class StripeSubscription : AggregateRoot
    {
        private StripeSubscription() { }
        public static StripeSubscription Create(
            DateTime? expirationDate,
            SubscriptionPlanType subscriptionPlanType,
            StripeSubscriptionStatus stripeSubscriptionStatus,
            Guid tenantId,
            StripeCustomer stripeCustomer)
        {
            return new StripeSubscription()
            {
                ExpirationDate = expirationDate,
                PlanType = subscriptionPlanType,
                Status = stripeSubscriptionStatus,
                TenantId = tenantId,
                StripeCustomer = stripeCustomer
            };
        }

        public Guid StripeCustomerId { get; set; }
        public StripeCustomer StripeCustomer { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public SubscriptionPlanType PlanType { get; set; }
        public StripeSubscriptionStatus Status { get; set; }
    }
}
