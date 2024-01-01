using Shared.Kernel.BuildingBlocks.Auth.Roles;

namespace Shared.Kernel.BuildingBlocks.Auth.Service
{
    public interface IAuthorizationService
    {
        void ThrowIfUserIsNotInRole(TenantRole role);
        TenantRole GetRoleOfUserInTenant();
    }
}
