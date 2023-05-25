using Shared.Kernel.DomainKernel;

namespace Shared.Kernel.BuildingBlocks.Authorization.Services
{
    public interface IUserAuthorizationService
    {
        void ThrowIfUserIsNotInRole(TenantRole role);
        TenantRole GetRoleOfUserInTenant();
    }
}
