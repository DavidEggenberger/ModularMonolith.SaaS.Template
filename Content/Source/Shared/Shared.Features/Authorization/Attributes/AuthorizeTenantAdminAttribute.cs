using Microsoft.AspNetCore.Authorization;

namespace WebShared.Misc.Attributes
{

    [Authorize(Policy = "TenantAdmin")]
    public class AuthorizeTenantAdminAttribute : Attribute
    {

    }
}
