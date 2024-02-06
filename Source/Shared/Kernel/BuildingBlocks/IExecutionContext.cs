using Microsoft.Extensions.Hosting;
using Shared.Kernel.BuildingBlocks.Auth;


namespace Shared.Kernel.BuildingBlocks
{
    public interface IExecutionContext
    {
        bool AuthenticatedRequest { get; }

        Guid UserId { get; }

        Guid TenantId { get; }

        SubscriptionPlanType TenantPlan { get; }

        TenantRole TenantRole { get; }

        IHostEnvironment HostingEnvironment { get; }

        public Uri BaseURI { get; }
    }
}
