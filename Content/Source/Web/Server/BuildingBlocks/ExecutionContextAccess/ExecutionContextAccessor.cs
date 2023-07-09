using Microsoft.AspNetCore.Http;
using Shared.Kernel.BuildingBlocks;
using Shared.Kernel.BuildingBlocks.Authorization;
using Shared.Kernel.Extensions;
using System;

namespace Shared.Infrastructure.DomainKernel.BuildingBlocks.ExecutionContextAccess
{
    public class ExecutionContextAccessor : IExecutionContextAccessor
    {
        private readonly HttpContext capturedHttpContext;

        public ExecutionContextAccessor(HttpContext httpContext)
        {
            this.capturedHttpContext = httpContext;
        }

        public Guid UserId
        {
            get
            {
                return capturedHttpContext.User.GetUserId<Guid>();
            }
        }

        public Guid TenantId
        {
            get
            {
                return capturedHttpContext.User.GetTenantId<Guid>();
            }
        }

        public SubscriptionPlanType TenantPlan
        {
            get
            {
                return capturedHttpContext.User.GetTenantSubscriptionPlanType();
            }
        }

        public TenantRole TenantRole
        {
            get
            {
                return capturedHttpContext.User.GetRoleInTenant();
            }
        }
    }
}
