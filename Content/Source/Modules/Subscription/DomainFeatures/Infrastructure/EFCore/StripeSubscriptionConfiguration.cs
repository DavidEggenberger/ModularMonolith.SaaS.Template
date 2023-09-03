using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Subscription.DomainFeatures.Aggregates.StripeSubscriptionAggregate.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Subscription.DomainFeatures.Infrastructure.EFCore
{
    public class StripeSubscriptionConfiguration : IEntityTypeConfiguration<StripeSubscription>
    {
        public void Configure(EntityTypeBuilder<StripeSubscription> builder)
        {
            
        }
    }
}
