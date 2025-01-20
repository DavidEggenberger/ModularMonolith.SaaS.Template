using Shared.Kernel.DomainKernel;
using System;

namespace Modules.TenantIdentity.Public.DTOs.Tenant.Operations
{
    public class InviteUserToTenantDTO
    {
        public Guid TenantId { get; set; }
        public Guid UserId { get; set; }
        public TenantRole Role { get; set; }
    }
}
