﻿using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Shared.DomainFeatures.CQRS.Query;
using Shared.Kernel.BuildingBlocks.Authorization.Constants;
using Modules.TenantIdentity.DomainFeatures.Aggregates.UserAggregate.Domain;
using Modules.TenantIdentity.DomainFeatures.Aggregates.UserAggregate.Application.Queries;

namespace Modules.TenantIdentity.DomainFeatures.Infrastructure
{
    public class ContextUserClaimsPrincipalFactory<TUser> : IUserClaimsPrincipalFactory<TUser> where TUser : ApplicationUser
    {
        private readonly IQueryDispatcher queryDispatcher;
        public ContextUserClaimsPrincipalFactory(IQueryDispatcher queryDispatcher)
        {
            this.queryDispatcher = queryDispatcher;
        }
        public async Task<ClaimsPrincipal> CreateAsync(TUser user)
        {
            var claimsForUserQuery = new GetClaimsForUser { UserId = user.Id };
            var claimsForUser = await queryDispatcher.DispatchAsync<GetClaimsForUser, IEnumerable<Claim>>(claimsForUserQuery);
            
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claimsForUser, IdentityConstants.ApplicationScheme, nameType: ClaimConstants.UserNameClaimType, ClaimConstants.UserRoleInTenantClaimType);
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            
            return claimsPrincipal;
        }
    }
}