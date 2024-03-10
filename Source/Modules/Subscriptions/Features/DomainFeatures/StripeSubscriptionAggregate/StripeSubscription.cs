using Modules.Subscriptions.Features.DomainFeatures.StripeCustomerAggregate;
using Shared.Features.Domain.AggregateRoot;
using Shared.Kernel.BuildingBlocks.Auth;

namespace Modules.Subscriptions.Features.DomainFeatures.StripeSubscriptionAggregate
{
    public class StripeSubscription : AggregateRoot
    {
        private StripeSubscription() { }

        public Guid StripeCustomerId { get; set; }
        public StripeCustomer StripeCustomer { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public SubscriptionPlanType PlanType { get; set; }
        public StripeSubscriptionStatus Status { get; set; }

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
    }
}
