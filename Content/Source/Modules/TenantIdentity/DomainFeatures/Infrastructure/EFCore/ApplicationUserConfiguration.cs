using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.TenantIdentity.DomainFeatures.UserAggregate.Domain;

namespace Modules.TenantIdentity.DomainFeatures.Infrastructure.EFCore
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            
        }
    }
}
