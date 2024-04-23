using Modules.Subscriptions.Features.Infrastructure.Configuration;
using Modules.Subscriptions.Features.Infrastructure.EFCore;
using Shared.Features.Modules;
using System.Reflection;

namespace Modules.Subscriptions.Features
{
    public class SubscriptionsModule : IModule
    {
        public Assembly FeaturesAssembly => typeof(SubscriptionsModule).Assembly;
        public SubscriptionsConfiguration SubscriptionsConfiguration { get; set; }
        public SubscriptionsDbContext SubscriptionsDbContext { get; set; }

        public SubscriptionsModule(SubscriptionsConfiguration configuration, SubscriptionsDbContext dbContext)
        {
            SubscriptionsConfiguration = configuration;
            SubscriptionsDbContext = dbContext;
        }
    }
}
