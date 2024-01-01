using Microsoft.AspNetCore.Authorization;

namespace Shared.Kernel.BuildingBlocks.Auth.Attributes
{
    [Authorize(Policy = "TenantUser")]
    public class AuthorizeTenantUserAttribute : Attribute
    {

    }
}
