using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.TenantIdentity.Features.Domain.TenantAggregate;

namespace Modules.TenantIdentity.Features.Infrastructure.EFCore.Configuration
{
    public class TenantConfiguration : IEntityTypeConfiguration<Tenant>
    {
        public void Configure(EntityTypeBuilder<Tenant> builder)
        {
            builder.Navigation(b => b.Memberships)
                .HasField("memberships")
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            //builder.Navigation(b => b.Invitations)
            //    .HasField("invitations")
            //    .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.OwnsMany(typeof(TenantInvitation), "invitations", o =>
            {
                o.WithOwner("Tenant").HasForeignKey("TenantId");
            });
        }
    }

    //public class TenantInvitationConfiguration : IEntityTypeConfiguration<TenantInvitation>
    //{
    //    //public void Configure(EntityTypeBuilder<TenantInvitation> builder)
    //    //{
    //    //    builder.HasKey(tenantInvation => new { tenantInvation.UserId });
    //    //}
    //}
}
