using Shared.Kernel.DomainKernel;

namespace Modules.TenantIdentity.Public.DTOs.Tenant.Operations
{
    public class ChangeRoleOfTenantMemberDTO
    {
        public TenantRole Role { get; set; }
    }
}
