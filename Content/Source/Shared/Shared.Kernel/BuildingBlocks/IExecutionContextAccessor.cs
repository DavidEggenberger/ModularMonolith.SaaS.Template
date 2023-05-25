using Shared.Features.BuildingBlocks.Interfaces;
using Shared.Kernel.BuildingBlocks.Authorization;

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
