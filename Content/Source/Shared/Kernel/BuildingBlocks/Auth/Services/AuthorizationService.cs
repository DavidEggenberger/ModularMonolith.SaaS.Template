using Shared.Kernel.BuildingBlocks;
using Shared.Kernel.BuildingBlocks.Auth.Exceptions;
using Shared.Kernel.BuildingBlocks.Authorization.Roles;
using Shared.Kernel.Extensions;

namespace Shared.Kernel.BuildingBlocks.Authorization.Service
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
