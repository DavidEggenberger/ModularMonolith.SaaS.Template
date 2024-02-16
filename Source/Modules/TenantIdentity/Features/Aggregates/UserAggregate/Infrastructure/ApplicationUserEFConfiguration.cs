using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.TenantIdentity.Features.Aggregates.UserAggregate;

namespace Modules.TenantIdentity.Features.Aggregates.UserAggregate.Infrastructure
{
    public class ApplicationUserEFConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {

        }
    }
}
