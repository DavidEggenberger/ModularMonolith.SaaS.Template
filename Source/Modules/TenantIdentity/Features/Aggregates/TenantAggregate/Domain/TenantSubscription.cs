using Modules.TenantIdentity.Features.Aggregates.TenantAggregate.Domain.Enums;
using Shared.Features.DomainKernel;
using Shared.Kernel.BuildingBlocks.Authorization;

namespace Modules.TenantIdentity.Features.Aggregates.TenantAggregate.Domain
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
