using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Modules.TenantIdentity.Features.DomainFeatures.Tenants.Domain;
using Shared.Kernel.BuildingBlocks.Auth;

namespace Modules.TenantIdentity.Features.DomainFeatures.Users
{
    public class ApplicationUser : IdentityUser<Guid>, IApplicationUser
    {
        private ApplicationUser() { }

        public string PictureUri { get; private set; }
        public bool IsOnline => CountOfOpenTabs > 0;
        public int CountOfOpenTabs { get; private set; }
        public Guid SelectedTenantId { get; private set; }
        public IList<TenantMembership> TenantMemberships { get; set; }

        public static ApplicationUser Create(string userName, string mail, string pictureUri)
        {
            return new ApplicationUser()
            {
                UserName = userName,
                Email = mail,
                PictureUri = pictureUri
            };
        }

        public void SetPictureUri(string pictureUri)
        {
            PictureUri = pictureUri;
        }

        public void SelectTenant(Guid tenantId)
        {
            SelectedTenantId = tenantId;
        }

        public void IncrementOpenTabCount()
        {
            CountOfOpenTabs++;
        }

        public void DecrementOpenTabCount()
        {
            if (CountOfOpenTabs == 0)
            {
                return;
            }
            CountOfOpenTabs--;
        }

        public virtual ICollection<IdentityUserLogin<Guid>> Logins { get; private set; }
        public virtual ICollection<IdentityUserClaim<Guid>> Claims { get; private set; }
        public virtual ICollection<IdentityUserToken<Guid>> Tokens { get; private set; }
    }

    public class ApplicationUserEFConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("ApplicationUser");
        }
    }
}
