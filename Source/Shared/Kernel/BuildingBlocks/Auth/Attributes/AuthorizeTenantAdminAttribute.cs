using Microsoft.AspNetCore.Authorization;

namespace Shared.Kernel.BuildingBlocks.Authorization.Attributes
{

    [Authorize(Policy = "TenantAdmin")]
    public class AuthorizeTenantAdminAttribute : Attribute
    {

    }
}
