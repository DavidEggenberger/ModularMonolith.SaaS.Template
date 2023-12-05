using Shared.Kernel.BuildingBlocks.Authorization.Roles;

namespace Shared.Kernel.BuildingBlocks.Authorization.Service
{
    public interface IAuthorizationService
    {
        void ThrowIfUserIsNotInRole(TenantRole role);
        TenantRole GetRoleOfUserInTenant();
    }
}
