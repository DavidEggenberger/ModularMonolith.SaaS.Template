using Modules.TenantIdentity.DomainFeatures.TenantAggregate.Domain;
using Modules.TenantIdentity.Web.Shared.DTOs.Aggregates.Tenant;
using Shared.Infrastructure.CQRS.Command;
using Shared.Infrastructure.CQRS.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.TenantIdentity.DomainFeatures.TenantAggregate.Application.Queries
{
    public class GetTenantDetailsByID : IQuery<Tenant>
    {
        public Guid TenantId { get; set; }
    }
}
