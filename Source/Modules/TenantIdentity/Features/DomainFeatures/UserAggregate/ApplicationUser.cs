using Microsoft.AspNetCore.Identity;
using Modules.TenantIdentity.Features.DomainFeatures.TenantAggregate.Domain;
using Modules.TenantIdentity.Features.DomainFeatures.TenantAggregate.Domain.Exceptions;
using Shared.Kernel.BuildingBlocks.Auth;

namespace Modules.TenantIdentity.Features.DomainFeatures.UserAggregate
{
    public class ApplicationUser : IdentityUser<Guid>, IApplicationUser
    {
        public string PictureUri { get; set; }
        public bool IsOnline => CountOfOpenTabs > 0;
        public int CountOfOpenTabs { get; set; }
        public Guid SelectedTenantId { get; set; }
        public IList<TenantMembership> TenantMemberships { get; set; }

        public void IncrementOpenTabCount()
        {
            CountOfOpenTabs++;
        }

        public void DecrementOpenTabCount()
        {
            if (CountOfOpenTabs == 0)
            {
                throw new TabsAlreadyClosedException("User has no tabs to close");
            }
            CountOfOpenTabs--;
        }

        public virtual ICollection<IdentityUserLogin<Guid>> Logins { get; set; }
        public virtual ICollection<IdentityUserClaim<Guid>> Claims { get; set; }
        public virtual ICollection<IdentityUserToken<Guid>> Tokens { get; set; }
    }
}
