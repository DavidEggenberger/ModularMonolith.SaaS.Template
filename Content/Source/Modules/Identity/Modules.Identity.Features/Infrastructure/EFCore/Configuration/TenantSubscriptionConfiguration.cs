using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.TenantIdentity.Features.TenantAggregate.Domain;

namespace Modules.TenantIdentityModule.Infrastructure.EFCore.Configuration
{
    internal class SubscriptionConfiguration : IEntityTypeConfiguration<TenantSubscription>
    {
        public void Configure(EntityTypeBuilder<TenantSubscription> builder)
        {

        }
    }
}
