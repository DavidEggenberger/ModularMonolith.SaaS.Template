using Shared.Kernel.BuildingBlocks.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.TenantIdentity.Web.Shared.DTOs.Aggregates.Tenant
{
    public class TenantMembershipDTO
    {
        public Guid UserId { get; set; }
        public Guid TenantId { get; set; }
        public TenantRole Role { get; set; }
    }
}
