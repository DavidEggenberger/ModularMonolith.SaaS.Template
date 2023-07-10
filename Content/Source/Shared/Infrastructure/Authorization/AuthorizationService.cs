using Microsoft.AspNetCore.Http;
using Shared.Kernel.BuildingBlocks;
using Shared.Kernel.BuildingBlocks.Authorization;
using Shared.Kernel.Exceptions.Authorization;
using Shared.Kernel.Extensions;

namespace Shared.Infrastructure.Authorization
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IExecutionContextAccessor executionContextAccessor;
        public AuthorizationService(IExecutionContextAccessor executionContextAccessor)
        {
            this.executionContextAccessor = executionContextAccessor;
        }

        public TenantRole GetRoleOfUserInTenant()
        {
            return executionContextAccessor.TenantRole;
        }

        public void ThrowIfUserIsNotInRole(TenantRole role)
        {
            if (executionContextAccessor.TenantRole != role)
            {
                throw new UnauthorizedException();
            }
        }
    }
}
