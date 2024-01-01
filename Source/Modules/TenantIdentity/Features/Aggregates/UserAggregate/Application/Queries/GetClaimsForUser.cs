using Modules.TenantIdentity.Features.Infrastructure.EFCore;
using Shared.Features.CQRS.Query;
using Shared.Kernel.BuildingBlocks.Auth.Constants;
using System.Security.Claims;
using System.Threading;

namespace Modules.TenantIdentity.Features.Aggregates.UserAggregate.Application.Queries
{
    public class GetClaimsForUser : IQuery<IEnumerable<Claim>>
    {
        public Guid UserId { get; set; }
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
            var user = await tenantIdentityDbContext.GetUserByIdAsync(query.UserId);

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimConstants.UserNameClaimType, user.UserName),
                new Claim(ClaimConstants.UserIdClaimType, user.Id.ToString()),
                new Claim(ClaimConstants.EmailClaimType, user.Email),
                new Claim(ClaimConstants.PictureClaimType, user.PictureUri)
            };

            var tenant = await tenantIdentityDbContext.GetTenantExtendedByIdAsync(user.SelectedTenantId);
            var tenantMembership = tenant.Memberships.Single(m => m.UserId == user.Id);

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
