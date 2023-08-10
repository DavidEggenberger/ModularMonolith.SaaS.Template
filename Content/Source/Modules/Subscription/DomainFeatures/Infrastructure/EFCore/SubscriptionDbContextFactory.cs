using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Modules.Subscription.DomainFeatures.StripeSubscriptionAggregate.Domain;
using Shared.Infrastructure.EFCore;
using Shared.Infrastructure.EFCore.Configuration;
using Shared.Infrastructure.MultiTenancy.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Subscription.DomainFeatures.Infrastructure.EFCore
{
    public class SubscriptionDbContextFactory : IDesignTimeDbContextFactory<SubscriptionDbContext>
    {
        public SubscriptionDbContext CreateDbContext(string[] args)
        {
            throw new NotImplementedException();
        }
    }
}
