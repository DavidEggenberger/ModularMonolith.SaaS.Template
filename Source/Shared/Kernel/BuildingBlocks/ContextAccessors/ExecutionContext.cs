using Shared.Kernel.BuildingBlocks.Auth;

namespace Shared.Kernel.BuildingBlocks.ContextAccessors
{
    public class ExecutionContext : IExecutionContext
    {
        public Guid UserId { get; set; }

        public Guid TenantId { get; set; }

        public SubscriptionPlanType TenantPlan { get; set; }

        public TenantRole TenantRole { get; set; }
    }
}
