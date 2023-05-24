using Microsoft.AspNetCore.Authorization;
using System;

namespace WebShared.Misc.Attributes
{
    [Authorize(Policy = "TenantUser")]
    public class AuthorizeTenantUserAttribute : Attribute
    {

    }
}
