using Shared.SharedKernel.Constants;
using Modules.TenantIdentityModule.Domain;
using Shared.Infrastructure.CQRS.Query;
using System.Security.Claims;
using System.Threading;

namespace Shared.Modules.TenantIdentityModule.Application.Queries
{
    public class ClaimsForUserQueryHandler : IQueryHandler<ClaimsForUserQuery, IEnumerable<Claim>>
    {
        private readonly IQueryDispatcher queryDispatcher;
        public ClaimsForUserQueryHandler(IQueryDispatcher queryDispatcher)
        {
            this.queryDispatcher = queryDispatcher;
        }
        public async Task<IEnumerable<Claim>> HandleAsync(ClaimsForUserQuery query, CancellationToken cancellation)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimConstants.UserNameClaimType, query.User.UserName),
                new Claim(ClaimConstants.UserIdClaimType, query.User.Id.ToString()),
                new Claim(ClaimConstants.EmailClaimType, query.User.Email),
                new Claim(ClaimConstants.PictureClaimType, query.User.PictureUri)
            };

            //var tenantByIdQuery = new GetTenantByQuery() { TenantId = query.User.SelectedTenantId };
            //Tenant currentTenant = await queryDispatcher.DispatchAsync<GetTenantByQuery, Tenant>(tenantByIdQuery);

            //var tenantMembershipQuery = new GetTenantMembershipQuery { TenantId = query.User.SelectedTenantId, UserId = query.User.Id };
            //TenantMembership tenantMembership = await queryDispatcher.DispatchAsync<GetTenantMembershipQuery, TenantMembership>(tenantMembershipQuery);

            //claims.AddRange(new List<Claim>
            //{
            //    new Claim(ClaimConstants.TenantPlanClaimType, currentTenant.CurrentSubscriptionPlanType.ToString()),
            //    new Claim(ClaimConstants.TenantNameClaimType, currentTenant.Name),
            //    new Claim(ClaimConstants.TenantIdClaimType, currentTenant.Id.ToString()),
            //    new Claim(ClaimConstants.UserRoleInTenantClaimType, tenantMembership.Role.ToString()),
            //});

            return claims;
        }
    }
}
