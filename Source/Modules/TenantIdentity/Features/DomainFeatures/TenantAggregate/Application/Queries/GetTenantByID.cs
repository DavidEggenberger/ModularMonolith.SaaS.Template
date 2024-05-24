using Modules.TenantIdentity.Web.Shared.DTOs.Tenant;
using Shared.Features.Messaging.Query;
using Shared.Features.Server;
using System.Threading;

namespace Modules.TenantIdentity.Features.DomainFeatures.TenantAggregate.Application.Queries
{
    public class GetTenantByID : IQuery<TenantDTO>
    {
        public Guid TenantId { get; set; }
    }
    public class GetTenantByIdQueryHandler : ServerExecutionBase<TenantIdentityModule>, IQueryHandler<GetTenantByID, TenantDTO>
    {
        public GetTenantByIdQueryHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }

        public async Task<TenantDTO> HandleAsync(GetTenantByID query, CancellationToken cancellation)
        {
            var tenant = await module.TenantIdentityDbContext.GetTenantByIdAsync(query.TenantId);
            return tenant.ToDTO();
        }
    }
}
