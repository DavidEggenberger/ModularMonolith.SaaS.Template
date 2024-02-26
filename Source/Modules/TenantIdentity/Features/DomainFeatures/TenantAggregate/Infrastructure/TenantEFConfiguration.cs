using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.TenantIdentity.Features.DomainFeatures.TenantAggregate.Domain;

namespace Modules.TenantIdentity.Features.DomainFeatures.TenantAggregate.Infrastructure
{
    public class TenantConfiguration : IEntityTypeConfiguration<Tenant>
    {
        public void Configure(EntityTypeBuilder<Tenant> builder)
        {
            builder.Navigation(b => b.Memberships)
                .HasField("memberships")
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Navigation(b => b.Invitations)
                .HasField("invitations")
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
