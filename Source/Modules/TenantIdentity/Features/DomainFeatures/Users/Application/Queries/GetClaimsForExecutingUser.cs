using Shared.Features.Messaging.Queries;
using Shared.Features.Misc.ExecutionContext;
using Shared.Kernel.Constants.Auth;
using System.Security.Claims;
using System.Threading;

namespace Modules.TenantIdentity.Features.DomainFeatures.Users.Application.Queries
{
    public class GetClaimsForExecutingUser : Query<IEnumerable<Claim>>
    {
    }

    public class ClaimsForUserQueryHandler : ServerExecutionBase<TenantIdentityModule>, IQueryHandler<GetClaimsForExecutingUser, IEnumerable<Claim>>
    {
        public ClaimsForUserQueryHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }

        public async Task<IEnumerable<Claim>> HandleAsync(GetClaimsForExecutingUser query, CancellationToken cancellation)
        {
            var user = await module.TenantIdentityDbContext.GetUserByIdAsync(executionContext.UserId);

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimConstants.UserNameClaimType, user.UserName),
                new Claim(ClaimConstants.UserIdClaimType, user.Id.ToString()),
                new Claim(ClaimConstants.EmailClaimType, user.Email),
                new Claim(ClaimConstants.PictureClaimType, user.PictureUri)
            };

            var tenant = await module.TenantIdentityDbContext.GetTenantExtendedByIdAsync(user.SelectedTenantId);
            var tenantMembership = tenant.Memberships.Single(m => m.UserId == user.Id);

            claims.AddRange(new List<Claim>
            {
                new Claim(ClaimConstants.TenantPlanClaimType, tenant.SubscriptionPlan.ToString()),
                new Claim(ClaimConstants.TenantNameClaimType, tenant.Name),
                new Claim(ClaimConstants.TenantIdClaimType, tenant.Id.ToString()),
                new Claim(ClaimConstants.UserRoleInTenantClaimType, tenantMembership.Role.ToString()),
            });

            return claims;
        }
    }
}
