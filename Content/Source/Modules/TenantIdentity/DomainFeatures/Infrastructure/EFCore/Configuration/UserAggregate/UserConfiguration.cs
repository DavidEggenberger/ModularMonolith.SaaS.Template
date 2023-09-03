using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.TenantIdentity.DomainFeatures.Aggregates.UserAggregate.Domain;

namespace Modules.TenantIdentity.DomainFeatures.Infrastructure.EFCore.Configuration.UserAggregate
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

        }
    }
}
