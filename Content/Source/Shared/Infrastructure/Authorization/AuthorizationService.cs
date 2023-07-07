using Microsoft.AspNetCore.Http;
using Shared.Kernel.BuildingBlocks.Authorization;
using Shared.Kernel.Exceptions.Authorization;
using Shared.Kernel.Extensions;

namespace Shared.Infrastructure.Authorization
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public AuthorizationService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        public TenantRole GetRoleOfUserInTenant()
        {
            return (TenantRole)Enum.Parse(typeof(TenantRole), httpContextAccessor.HttpContext.User.GetRoleClaim());
        }

        public void ThrowIfUserIsNotInRole(TenantRole role)
        {
            if (httpContextAccessor.HttpContext.User.GetRoleClaim() != role.ToString())
            {
                throw new UnauthorizedException();
            }
        }
    }
}
