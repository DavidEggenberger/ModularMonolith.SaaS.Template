using Microsoft.AspNetCore.Authorization;

namespace Shared.Kernel.BuildingBlocks.Authorization.Attributes
{
    [Authorize(Policy = "TenantUser")]
    public class AuthorizeTenantUserAttribute : Attribute
    {

    }
}
