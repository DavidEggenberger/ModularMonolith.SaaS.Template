using Shared.Kernel.BuildingBlocks.Authorization.Roles;
using System;

namespace Modules.TenantIdentity.Web.Shared.DTOs.Aggregates.Tenant
{
    public class TenantMembershipDTO
    {
        public Guid UserId { get; set; }
        public Guid TenantId { get; set; }
        public TenantRole Role { get; set; }
    }
}
