using Modules.TenantIdentity.DomainFeatures.Aggregates.TenantAggregate.Domain.Enums;
using Shared.Infrastructure.DomainKernel;
using Shared.Kernel.BuildingBlocks.Authorization;

namespace Modules.TenantIdentity.DomainFeatures.Aggregates.TenantAggregate.Domain
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
