using Modules.TenantIdentity.DomainFeatures.Infrastructure.EFCore;
using Modules.TenantIdentity.DomainFeatures.UserAggregate.Domain;
using Shared.Infrastructure.CQRS.Query;
using Shared.Kernel.BuildingBlocks.Authorization.Constants;
using System.Security.Claims;
using System.Threading;

namespace Modules.TenantIdentity.DomainFeatures.UserAggregate.Application.Queries
{
    public class GetClaimsForUser : IQuery<IEnumerable<Claim>>
    {
        public User User { get; set; }
    }

    public class ClaimsForUserQueryHandler : IQueryHandler<GetClaimsForUser, IEnumerable<Claim>>
    {
        private readonly IQueryDispatcher queryDispatcher;
        private readonly TenantIdentityDbContext tenantIdentityDbContext;

        public ClaimsForUserQueryHandler(IQueryDispatcher queryDispatcher, TenantIdentityDbContext tenantIdentityDbContext)
        {
            this.queryDispatcher = queryDispatcher;
            this.tenantIdentityDbContext = tenantIdentityDbContext;
        }
        public async Task<IEnumerable<Claim>> HandleAsync(GetClaimsForUser query, CancellationToken cancellation)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimConstants.UserNameClaimType, query.User.UserName),
                new Claim(ClaimConstants.UserIdClaimType, query.User.Id.ToString()),
                new Claim(ClaimConstants.EmailClaimType, query.User.Email),
                new Claim(ClaimConstants.PictureClaimType, query.User.PictureUri)
            };

            var tenant = await tenantIdentityDbContext.GetTenantExtendedByIdAsync(query.User.SelectedTenantId);
            var tenantMembership = tenant.Memberships.Single(m => m.UserId == query.User.Id);

            claims.AddRange(new List<Claim>
            {
                new Claim(ClaimConstants.TenantPlanClaimType, tenant.CurrentSubscriptionPlanType.ToString()),
                new Claim(ClaimConstants.TenantNameClaimType, tenant.Name),
                new Claim(ClaimConstants.TenantIdClaimType, tenant.Id.ToString()),
                new Claim(ClaimConstants.UserRoleInTenantClaimType, tenantMembership.Role.ToString()),
            });

            return claims;
        }
    }
}
