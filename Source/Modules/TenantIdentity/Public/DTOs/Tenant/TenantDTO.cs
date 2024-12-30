using Shared.Kernel.DomainKernel;
using System.Collections.Generic;

namespace Modules.TenantIdentity.Public.DTOs.Tenant
{
    public class TenantDTO
    {
        public SubscriptionPlanType SubscriptionPlanType { get; set; }
        public string Name { get; set; }
    }
}
