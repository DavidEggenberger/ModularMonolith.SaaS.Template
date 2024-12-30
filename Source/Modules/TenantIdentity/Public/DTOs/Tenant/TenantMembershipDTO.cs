using Shared.Kernel.DomainKernel;
using System;

namespace Modules.TenantIdentity.Public.DTOs.Tenant
{
    public class TenantMembershipDTO
    {
        public Guid UserId { get; set; }
        public Guid TenantId { get; set; }
        public TenantRole Role { get; set; }
    }
}
