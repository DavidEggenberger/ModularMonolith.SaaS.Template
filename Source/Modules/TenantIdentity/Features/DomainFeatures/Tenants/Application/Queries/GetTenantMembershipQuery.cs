using System.Threading;
using Shared.Features.Server;
using Shared.Features.Messaging.Queries;
using Modules.TenantIdentity.Public.DTOs.Tenant;

namespace Modules.TenantIdentity.Features.DomainFeatures.Tenants.Application.Queries
{
    public class GetTenantMembershipQuery : Query<TenantMembershipDTO>
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
