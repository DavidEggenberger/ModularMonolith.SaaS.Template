using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Modules.IdentityModule.Domain.Exceptions;
using Modules.TenantIdentityModule.Infrastructure.EFCore;
using Modules.TenantIdentity.Features.UserAggregate.Domain;
using System.Security.Claims;

namespace Modules.TenantIdentityModule.Layers.Infrastructure
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        private readonly TenantIdentityDbContext identificationDbContext;
        public ApplicationUserManager(TenantIdentityDbContext identificationDbContext, IUserStore<ApplicationUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<ApplicationUser> passwordHasher, IEnumerable<IUserValidator<ApplicationUser>> userValidators, IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<ApplicationUserManager> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            this.identificationDbContext = identificationDbContext;
        }

        public async Task<ApplicationUser> FindByClaimsPrincipalAsync(ClaimsPrincipal claimsPrincipal)
        {
            ApplicationUser user = await base.GetUserAsync(claimsPrincipal);
            if (user == null)
            {
                throw new IdentityOperationException();
            }

            return user;
        }
        public async Task<ApplicationUser> FindByIdAsync(Guid id)
        {
            ApplicationUser applicationUser;
            try
            {
                applicationUser = await identificationDbContext.Users.SingleAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
            return applicationUser;
        }

        public Task FindByLoginAsync(string loginProvider, string providerKey)
        {
            throw new NotImplementedException();
        }

        public async Task<ApplicationUser> FindUserByStripeCustomerId(string stripeCustomerId)
        {
            ApplicationUser applicationUser;
            try
            {
                applicationUser = await identificationDbContext.Users.SingleAsync(u => u.StripeCustomerId == stripeCustomerId);
                return applicationUser;
            }
            catch (Exception ex)
            {
                throw new IdentityOperationException();
            }
        }
        public async Task SetTenantAsSelected(ApplicationUser applicationUser, Guid tenantId)
        {
            applicationUser.SelectedTenantId = tenantId;
            await identificationDbContext.SaveChangesAsync();
        }
    }
}
