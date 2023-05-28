using Domain.Aggregates.TenantAggregate.Enums;
using Shared.Domain;

namespace Modules.TetnantIdentity.Features.TenantAggregate.Domain
{
    public class TenantSubscription : Entity
    {
        public string StripeSubscriptionId { get; set; }
        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }
        public SubscriptionPlanType SubscriptionPlanType { get; set; }
        public SubscriptionStatus Status { get; set; }
    }
}
