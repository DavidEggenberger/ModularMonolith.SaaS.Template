using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.TenantIdentity.Features.Domain.TenantAggregate;

namespace Modules.TenantIdentity.Features.Infrastructure.EFCore.Configuration.TenantAggregate
{
    internal class SubscriptionConfiguration : IEntityTypeConfiguration<TenantSubscription>
    {
        public void Configure(EntityTypeBuilder<TenantSubscription> builder)
        {

        }
    }
}
