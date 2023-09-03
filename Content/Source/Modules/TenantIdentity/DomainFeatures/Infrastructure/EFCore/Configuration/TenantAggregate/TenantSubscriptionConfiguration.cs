using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.TenantIdentity.DomainFeatures.Aggregates.TenantAggregate.Domain;

namespace Modules.TenantIdentity.DomainFeatures.Infrastructure.EFCore.Configuration.TenantAggregate
{
    internal class SubscriptionConfiguration : IEntityTypeConfiguration<TenantSubscription>
    {
        public void Configure(EntityTypeBuilder<TenantSubscription> builder)
        {

        }
    }
}
