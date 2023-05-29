using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.IdentityModule.Domain;
using Modules.TenantIdentity.Features.UserAggregate.Domain;

namespace Modules.TenantIdentityModule.Infrastructure.EFCore
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            
        }
    }
}
