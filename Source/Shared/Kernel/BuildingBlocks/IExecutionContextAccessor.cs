using Shared.Kernel.BuildingBlocks.Auth;
using Shared.Kernel.BuildingBlocks.Auth.Roles;

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
