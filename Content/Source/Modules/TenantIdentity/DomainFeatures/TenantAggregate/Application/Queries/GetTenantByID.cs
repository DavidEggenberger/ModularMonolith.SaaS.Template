using Modules.TenantIdentity.DomainFeatures.TenantAggregate.Domain;
using Shared.Infrastructure.CQRS.Query;

namespace Modules.TenantIdentity.DomainFeatures.Application.Queries
{
    public class GetTenantByID : IQuery<Tenant>
    {
        public Guid TenantId { get; set; }
    }
}
