using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Shared.Kernel.DomainKernel;
using Shared.Features.Misc;

namespace Modules.TenantIdentity.Features.DomainFeatures.Tenants.Domain
{
    public class TenantInvitation : Entity
    {
        public Tenant Tenant { get; private set; }
        public string Email { get; private set; }
        public TenantRole Role { get; private set; }

        public static TenantInvitation Create(Tenant tenant, string email, TenantRole role)
        {
            return new TenantInvitation()
            {
                Email = email,
                Role = role,
                Tenant = tenant
            };
        }
    }

    public class TenantInvitationEFConfiguration : IEntityTypeConfiguration<TenantInvitation>
    {
        public void Configure(EntityTypeBuilder<TenantInvitation> builder)
        {
            builder.ToTable("TenantInvitation");
        }
    }
}
