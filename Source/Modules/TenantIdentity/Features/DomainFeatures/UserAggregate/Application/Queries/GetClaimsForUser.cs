using Shared.Features.Messaging.Query;
using Shared.Features.Server;
using Shared.Kernel.BuildingBlocks.Auth.Constants;
using System.Security.Claims;
using System.Threading;

namespace Modules.TenantIdentity.Features.DomainFeatures.UserAggregate.Application.Queries
{
    public class GetClaimsForUser : IQuery<IEnumerable<Claim>>
    {
        public Guid UserId { get; set; }
    }

    public class ClaimsForUserQueryHandler : ServerExecutionBase<TenantIdentityModule>, IQueryHandler<GetClaimsForUser, IEnumerable<Claim>>
    {
        public ClaimsForUserQueryHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }

        public async Task<IEnumerable<Claim>> HandleAsync(GetClaimsForUser query, CancellationToken cancellation)
        {
            var user = await module.TenantIdentityDbContext.GetUserByIdAsync(query.UserId);

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
                new Claim(ClaimConstants.TenantPlanClaimType, tenant.SubscriptionPlanType.ToString()),
                new Claim(ClaimConstants.TenantNameClaimType, tenant.Name),
                new Claim(ClaimConstants.TenantIdClaimType, tenant.Id.ToString()),
                new Claim(ClaimConstants.UserRoleInTenantClaimType, tenantMembership.Role.ToString()),
            });

            return claims;
        }
    }
}
