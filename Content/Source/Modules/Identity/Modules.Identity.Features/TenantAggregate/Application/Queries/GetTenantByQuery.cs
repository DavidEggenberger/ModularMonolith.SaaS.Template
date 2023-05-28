using Modules.TenantIdentityModule.Domain;
using Shared.Infrastructure.CQRS.Query;

namespace Shared.Modules.TenantIdentityModule.Application.Queries
{
    public class GetTenantByQuery : IQuery<Tenant>
    {
        public Guid TenantId { get; set; }
    }
}
