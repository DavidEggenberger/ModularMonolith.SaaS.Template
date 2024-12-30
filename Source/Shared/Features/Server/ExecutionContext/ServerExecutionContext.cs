using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared.Kernel.BuildingBlocks;
using Shared.Kernel.DomainKernel;
using Shared.Kernel.Extensions.ClaimsPrincipal;

namespace Shared.Features.Server.ExecutionContext
{
    public class ServerExecutionContext : IExecutionContext
    {
        private static ServerExecutionContext executionContext;
        private ServerExecutionContext() { }

        public bool AuthenticatedRequest { get; private set; }
        public Guid UserId { get; private set; }
        public Guid TenantId { get; private set; }
        public SubscriptionPlanType TenantPlan { get; private set; }
        public TenantRole TenantRole { get; private set; }
        public IHostEnvironment HostingEnvironment { get; set; }
        public Uri BaseURI { get; private set; }

        public static ServerExecutionContext CreateInstance(IServiceProvider serviceProvider)
        {
            if (executionContext is not null)
            {
                return executionContext;
            }

            return new ServerExecutionContext()
            {
                HostingEnvironment = serviceProvider.GetRequiredService<IHostEnvironment>()
            };
        }

        public void InitializeInstance(HttpContext httpContext)
        {
            var server = httpContext.RequestServices.GetRequiredService<IServer>();
            var addresses = server?.Features.Get<IServerAddressesFeature>();

            BaseURI = new Uri(addresses?.Addresses.FirstOrDefault(a => a.Contains("https")) ?? string.Empty);

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
    }
}
