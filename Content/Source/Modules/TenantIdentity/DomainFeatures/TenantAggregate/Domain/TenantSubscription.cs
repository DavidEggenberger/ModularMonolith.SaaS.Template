using Modules.TenantIdentity.DomainFeatures.Domain.Exceptions;
using Shared.DomainFeatures;
using Shared.Kernel.BuildingBlocks.Authorization;

namespace Modules.TenantIdentity.DomainFeatures.TenantAggregate.Domain
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
