using Shared.Kernel.DomainKernel;

namespace Shared.Kernel.Interfaces
{
    public interface IExecutionContextAccessor
    {
        Guid UserId { get; }

        Guid TenantId { get; }

        SubscriptionPlanType TenantPlan { get; }

        TenantRole TenantRole { get; }
    }
}
