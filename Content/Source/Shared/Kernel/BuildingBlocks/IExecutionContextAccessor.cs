using Shared.Kernel.BuildingBlocks.Authorization;
using Shared.Kernel.BuildingBlocks.Authorization.Roles;

namespace Shared.Kernel.BuildingBlocks
{
    public interface IExecutionContextAccessor
    {
        Guid UserId { get; }

        Guid TenantId { get; }

        SubscriptionPlanType TenantPlan { get; }

        TenantRole TenantRole { get; }
    }
}
