using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.TenantIdentity.Features.TenantAggregate.Domain;

namespace Modules.TenantIdentityModule.Infrastructure.EFCore.Configuration
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
