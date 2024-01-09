using Modules.TenantIdentity.Features.Domain.TenantAggregate.Enums;
using Shared.Features.DomainKernel;
using Shared.Kernel.BuildingBlocks.Auth;

namespace Modules.TenantIdentity.Features.Domain.TenantAggregate
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
