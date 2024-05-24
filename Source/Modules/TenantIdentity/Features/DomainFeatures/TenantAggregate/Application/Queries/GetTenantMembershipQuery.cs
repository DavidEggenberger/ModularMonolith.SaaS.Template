using Shared.Features.Messaging.Query;
using System.Threading;
using Modules.TenantIdentity.Web.Shared.DTOs.Tenant;
using Shared.Features.Server;

namespace Modules.TenantIdentity.Features.DomainFeatures.TenantAggregate.Application.Queries
{
    public class GetTenantMembershipQuery : IQuery<TenantMembershipDTO>
    {
        public Guid UserId { get; set; }
        public Guid TenantId { get; set; }
    }
    public class GetTenantMembershipQueryHandler : ServerExecutionBase<TenantIdentityModule>, IQueryHandler<GetTenantMembershipQuery, TenantMembershipDTO>
    {
        public GetTenantMembershipQueryHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }

        public async Task<TenantMembershipDTO> HandleAsync(GetTenantMembershipQuery query, CancellationToken cancellation)
        {
            var tenantMembership = module.TenantIdentityDbContext.TenantMeberships.Single(m => m.UserId == query.UserId);
            return tenantMembership.ToDTO();
        }
    }
}
