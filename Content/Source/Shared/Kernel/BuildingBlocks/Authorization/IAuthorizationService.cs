namespace Shared.Kernel.BuildingBlocks.Authorization
{
    public interface IAuthorizationService
    {
        void ThrowIfUserIsNotInRole(TenantRole role);
        TenantRole GetRoleOfUserInTenant();
    }
}
