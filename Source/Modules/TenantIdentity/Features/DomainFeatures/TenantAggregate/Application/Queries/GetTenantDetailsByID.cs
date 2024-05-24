using Microsoft.EntityFrameworkCore;
using Modules.TenantIdentity.Web.Shared.DTOs.Tenant;
using Shared.Features.Messaging.Query;
using Shared.Features.Server;
using System.Threading;

namespace Modules.TenantIdentity.Features.DomainFeatures.TenantAggregate.Application.Queries
{
    public class GetTenantDetailsByID : IQuery<TenantDetailDTO>
    {
        public Guid TenantId { get; set; }
    }
    public class GetTenantDetailsByIDQueryHandler : ServerExecutionBase<TenantIdentityModule>, IQueryHandler<GetTenantDetailsByID, TenantDetailDTO>
    {
        public GetTenantDetailsByIDQueryHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }

        public async Task<TenantDetailDTO> HandleAsync(GetTenantDetailsByID query, CancellationToken cancellation)
        {
            var tenantDetail = await module.TenantIdentityDbContext.Tenants.Where(t => t.TenantId == query.TenantId).SingleAsync();
            return tenantDetail.ToDetailDTO();
        }
    }
}
