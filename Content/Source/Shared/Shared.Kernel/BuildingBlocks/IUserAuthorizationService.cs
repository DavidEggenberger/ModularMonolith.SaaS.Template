using Shared.Kernel.DomainKernel;

namespace Shared.Kernel.BuildingBlocks
{
    public interface IUserAuthorizationService
    {
        void ThrowIfUserIsNotInRole(TenantRole role);
        TenantRole GetRoleOfUserInTenant();
    }
}
