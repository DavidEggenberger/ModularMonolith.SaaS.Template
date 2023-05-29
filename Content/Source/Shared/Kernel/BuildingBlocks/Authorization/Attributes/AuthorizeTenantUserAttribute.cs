using Microsoft.AspNetCore.Authorization;
using System;

namespace Shared.Kernel.BuildingBlocks.Authorization.Attributes
{
    [Authorize(Policy = "TenantUser")]
    public class AuthorizeTenantUserAttribute : Attribute
    {

    }
}
