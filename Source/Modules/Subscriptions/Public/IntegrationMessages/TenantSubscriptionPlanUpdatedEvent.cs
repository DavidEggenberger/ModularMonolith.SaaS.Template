using Shared.Kernel.BuildingBlocks;
using Shared.Kernel.DomainKernel;

namespace Modules.Subscriptions.Public.IntegrationMessages
{
    public class TenantSubscriptionPlanUpdatedEvent : IIntegrationEvent
    {
        public Guid TenantId { get; set; }
        public SubscriptionPlanType SubscriptionPlanType { get; set; }
    }
}
