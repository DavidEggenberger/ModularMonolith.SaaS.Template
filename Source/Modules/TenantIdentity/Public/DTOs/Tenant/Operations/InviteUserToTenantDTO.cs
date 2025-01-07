using Shared.Kernel.DomainKernel;
using System;
using System.Reflection.Metadata.Ecma335;

namespace Modules.TenantIdentity.Public.DTOs.Tenant.Operations
{
    public class InviteUserToTenantDTO
    {
        public Guid TenantId { get; set; }
        public Guid UserId { get; set; }
        public TenantRole Role { get; set; }
    }
}
