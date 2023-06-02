using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Modules.TenantIdentity.DomainFeatures.UserAggregate.Domain;
using Modules.TenantIdentity.DomainFeatures.Infrastructure.EFCore;
using Modules.TenantIdentity.DomainFeatures.Domain.Exceptions;

namespace Modules.TenantIdentity.DomainFeatures.Infrastructure
{
    public class ApplicationUserManager : UserManager<User>
    {
        private readonly TenantIdentityDbContext identificationDbContext;
        public ApplicationUserManager(TenantIdentityDbContext identificationDbContext, IUserStore<User> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<User> passwordHasher, IEnumerable<IUserValidator<User>> userValidators, IEnumerable<IPasswordValidator<User>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<ApplicationUserManager> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            this.identificationDbContext = identificationDbContext;
        }

        public async Task<User> FindByClaimsPrincipalAsync(ClaimsPrincipal claimsPrincipal)
        {
            User user = await base.GetUserAsync(claimsPrincipal);
            if (user == null)
            {
                throw new IdentityOperationException();
            }

            return user;
        }
        public async Task<User> FindByIdAsync(Guid id)
        {
            User applicationUser;
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

        public async Task<User> FindUserByStripeCustomerId(string stripeCustomerId)
        {
            User applicationUser;
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
        public async Task SetTenantAsSelected(User applicationUser, Guid tenantId)
        {
            applicationUser.SelectedTenantId = tenantId;
            await identificationDbContext.SaveChangesAsync();
        }
    }
}
