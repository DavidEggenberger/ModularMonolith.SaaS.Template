using Modules.Subscriptions.Features.Aggregates.StripeCustomerAggregate;
using Shared.Features.DomainKernel;
using Shared.Kernel.BuildingBlocks.Auth;
using System;

namespace Modules.Subscriptions.Features.Aggregates.StripeSubscriptionAggregate
{
    public class StripeSubscription : Entity
    {
        private StripeSubscription() { }
        public static StripeSubscription Create(
            DateTime? expirationDate,
            SubscriptionPlanType subscriptionPlanType,
            StripeSubscriptionStatus stripeSubscriptionStatus,
            StripeCustomer stripeCustomer)
        {
            return new StripeSubscription()
            {
                ExpirationDate = expirationDate,
                PlanType = subscriptionPlanType,
                Status = stripeSubscriptionStatus,
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
