using Shared.Kernel.DomainKernel;

namespace Modules.TenantIdentity.Public.DTOs.Tenant
{
    public class TenantDTO
    {
        public SubscriptionPlanType SubscriptionPlanType { get; set; }
        public string Name { get; set; }
    }
}
