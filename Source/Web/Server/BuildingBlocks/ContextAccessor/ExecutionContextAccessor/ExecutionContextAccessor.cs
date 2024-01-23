using Microsoft.AspNetCore.Http;
using Shared.Kernel.BuildingBlocks.ContextAccessors;
using Shared.Kernel.Extensions.ClaimsPrincipal;
using System;

namespace Web.Server.BuildingBlocks.ContextAccessor.ExecutionContextAccessor
{
    public class ExecutionContextAccessor : IExecutionContextAccessor
    {
        private HttpContext capturedHttpContext;

        public void CaptureHttpContext(HttpContext httpContext)
        {
            capturedHttpContext = httpContext;
        }

        public IExecutionContext ExecutionContext
        {
            get => new ExecutionContext
            {
                UserId = capturedHttpContext.User.GetUserId<Guid>(),
                TenantId = capturedHttpContext.User.GetTenantId<Guid>(),
                TenantPlan = capturedHttpContext.User.GetTenantSubscriptionPlanType(),
                TenantRole = capturedHttpContext.User.GetTenantRole()
            };
        }
    }
}
