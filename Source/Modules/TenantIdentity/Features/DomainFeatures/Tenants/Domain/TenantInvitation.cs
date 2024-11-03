using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Shared.Features.Domain;
using Shared.Kernel.BuildingBlocks.Auth;

namespace Modules.TenantIdentity.Features.DomainFeatures.Tenants.Domain
{
    public class TenantInvitation : Entity
    {
        public Guid TenantId { get; set; }
        public Tenant Tenant { get; set; }
        public string Email { get; set; }
        public TenantRole Role { get; set; }
    }

    public class TenantInvitationEFConfiguration : IEntityTypeConfiguration<TenantInvitation>
    {
        public void Configure(EntityTypeBuilder<TenantInvitation> builder)
        {
            builder.ToTable("TenantInvitation");
        }
    }
}
