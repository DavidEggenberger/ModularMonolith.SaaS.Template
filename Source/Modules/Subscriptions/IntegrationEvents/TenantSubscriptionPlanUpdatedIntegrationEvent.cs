using Shared.Kernel.BuildingBlocks;
using Shared.Kernel.DomainKernel;

namespace Modules.Subscriptions.IntegrationEvents
{
    public class TenantSubscriptionPlanUpdatedIntegrationEvent : IIntegrationEvent
    {
        public Guid TenantId { get; set; }
        public SubscriptionPlanType SubscriptionPlanType { get; set; }
    }
}
