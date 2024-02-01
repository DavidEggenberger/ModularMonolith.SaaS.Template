using Microsoft.Extensions.Hosting;
using Shared.Kernel.BuildingBlocks.Auth;


namespace Shared.Kernel.BuildingBlocks.ContextAccessors
{
    public interface IExecutionContext
    {
        Guid UserId { get; }

        Guid TenantId { get; }

        SubscriptionPlanType TenantPlan { get; }

        TenantRole TenantRole { get; }

        IHostEnvironment HostingEnvironment { get; }
    }
}
