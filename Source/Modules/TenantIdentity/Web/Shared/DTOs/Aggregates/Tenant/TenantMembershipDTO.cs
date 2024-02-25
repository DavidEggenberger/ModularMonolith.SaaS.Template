using Shared.Kernel.BuildingBlocks.Auth;
using System;

namespace Modules.TenantIdentity.Web.Shared.DTOs.Tenant
{
    public class TenantMembershipDTO
    {
        public Guid UserId { get; set; }
        public Guid TenantId { get; set; }
        public TenantRole Role { get; set; }
    }
}
