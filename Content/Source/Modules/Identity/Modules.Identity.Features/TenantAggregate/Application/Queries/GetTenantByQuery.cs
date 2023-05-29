using Modules.TenantIdentity.Features.TenantAggregate.Domain;
using Shared.Features.Infrastructure.CQRS.Query;

namespace Shared.Modules.TenantIdentityModule.Application.Queries
{
    public class GetTenantByQuery : IQuery<Tenant>
    {
        public Guid TenantId { get; set; }
    }
}
