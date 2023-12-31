using Modules.Subscriptions.Features.Agregates.StripeCustomerAggregate;
using Shared.Features.DomainKernel;
using Shared.Kernel.BuildingBlocks.Authorization;

namespace Modules.Subscriptions.Features.Agregates.StripeSubscriptionAggregate
{
    public class StripeSubscription : AggregateRoot
    {
        public StripeCustomer StripeCustomer { get; set; }
        public DateTime ExpirationDate { get; set; }
        public SubscriptionPlanType PlanType { get; set; }
        public SubscriptionStatus Status { get; set; }
    }
}
