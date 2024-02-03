using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared.Kernel.BuildingBlocks.Auth;
using Shared.Kernel.Extensions.ClaimsPrincipal;

namespace Shared.Kernel.BuildingBlocks.ExecutionContext
{
    public class ExecutionContext : IExecutionContext
    {
        private static ExecutionContext executionContext;
        private ExecutionContext() { }

        public static ExecutionContext GetInstance(IServiceProvider serviceProvider)
        {
            if (executionContext != null)
            {
                return executionContext;
            }

            var server = serviceProvider.GetService<IServer>();
            var addresses = server?.Features.Get<IServerAddressesFeature>();

            return new ExecutionContext
            {
                BaseURI = new Uri(addresses?.Addresses.FirstOrDefault(a => a.Contains("https")) ?? string.Empty)
            };
        }

        public void CaptureHttpContext(HttpContext httpContext)
        {
            if (httpContext.User.Identity.IsAuthenticated is false)
            {
                AuthenticatedRequest = false;
                return;
            }

            UserId = httpContext.User.GetUserId<Guid>();
            TenantId = httpContext.User.GetTenantId<Guid>();
            TenantPlan = httpContext.User.GetTenantSubscriptionPlanType();
            TenantRole = httpContext.User.GetTenantRole();
        }

        public bool AuthenticatedRequest { get; private set; }

        public Guid UserId { get; private set; }

        public Guid TenantId { get; private set; }

        public SubscriptionPlanType TenantPlan { get; private set; }

        public TenantRole TenantRole { get; private set; }

        public IHostEnvironment HostingEnvironment { get; private set; }

        public Uri BaseURI { get; private set; }
    }
}
