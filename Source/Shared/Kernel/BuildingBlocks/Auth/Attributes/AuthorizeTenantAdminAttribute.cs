using Microsoft.AspNetCore.Authorization;

namespace Shared.Kernel.BuildingBlocks.Auth.Attributes
{

    [Authorize(Policy = "TenantAdmin")]
    public class AuthorizeTenantAdminAttribute : Attribute
    {

    }
}
