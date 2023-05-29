using Microsoft.AspNetCore.Identity;
using Modules.TenantIdentity.DomainFeatures.Domain.Exceptions;

namespace Modules.TenantIdentity.DomainFeatures.UserAggregate.Domain
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string PictureUri { get; set; }
        public bool IsOnline => CountOfOpenTabs > 0;
        public int CountOfOpenTabs { get; set; }
        public string StripeCustomerId { get; set; }
        public Guid SelectedTenantId { get; set; }

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
