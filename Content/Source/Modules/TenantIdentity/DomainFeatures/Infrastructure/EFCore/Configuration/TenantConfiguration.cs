using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.TenantIdentity.DomainFeatures.TenantAggregate.Domain;

namespace Modules.TenantIdentity.DomainFeatures.Infrastructure.EFCore.Configuration
{
    public class TenantConfiguration : IEntityTypeConfiguration<Tenant>
    {
        public void Configure(EntityTypeBuilder<Tenant> builder)
        {
            builder.Navigation(b => b.Memberships)
                .HasField("memberships")
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
