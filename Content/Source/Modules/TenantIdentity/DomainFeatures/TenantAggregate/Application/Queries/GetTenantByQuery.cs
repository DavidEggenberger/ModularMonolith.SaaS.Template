using Modules.TenantIdentity.DomainFeatures.TenantAggregate.Domain;
using Shared.DomainFeatures.Infrastructure.CQRS.Query;

namespace Modules.TenantIdentity.DomainFeatures.Application.Queries
{
    public class GetTenantByQuery : IQuery<Tenant>
    {
        public Guid TenantId { get; set; }
    }
}
