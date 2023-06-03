using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Shared.Infrastructure.CQRS.Query;
using Shared.Kernel.BuildingBlocks.Authorization.Constants;
using Modules.TenantIdentity.DomainFeatures.UserAggregate.Domain;

namespace Modules.TenantIdentity.DomainFeatures.Infrastructure
{
    public class UserClaimsPrincipalFactory<User> : IUserClaimsPrincipalFactory<User> where User : UserAggregate.Domain.User
    {
        private readonly UserManager applicationUserManager;
        private readonly IQueryDispatcher queryDispatcher;
        public UserClaimsPrincipalFactory(UserManager applicationUserManager, IQueryDispatcher queryDispatcher)
        {
            this.applicationUserManager = applicationUserManager;
            this.queryDispatcher = queryDispatcher;
        }
        public async Task<ClaimsPrincipal> CreateAsync(User user)
        {
            UserAggregate.Domain.User applicationUser = await applicationUserManager.FindByIdAsync(user.Id);

            //var claimsForUserQuery = new ClaimsForUserQuery { User = applicationUser };
            //var claimsForUserQuery = new ClaimsForUserQuery();
            //var claimsForUser = await queryDispatcher.DispatchAsync<ClaimsForUserQuery, IEnumerable<Claim>>(claimsForUserQuery);
            var claimsForUser = await applicationUserManager.GetClaimsAsync(applicationUser);
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claimsForUser, IdentityConstants.ApplicationScheme, nameType: ClaimConstants.UserNameClaimType, ClaimConstants.UserRoleInTenantClaimType);
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            
            return claimsPrincipal;
        }
    }
}
