using Shared.Kernel.BuildingBlocks.Authorization;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Subscription.DomainFeatures.StripeSubscriptionAggregate.Domain
{
    public class StripeSubscription
    {
        public Guid TenantId { get; set; }
        public StripeCustomer StripeCustomer { get; set; }
        public DateTime ExpirationDate { get; set; }
        public SubscriptionPlanType PlanType { get; set; }
        public StripeSubscriptionStatus Status { get; set; }
    }
}
