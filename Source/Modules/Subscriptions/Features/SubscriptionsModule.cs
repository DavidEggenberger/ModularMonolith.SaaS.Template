using Shared.Features.Modules;
using System.Reflection;

namespace Modules.Subscriptions.Features
{
    public class SubscriptionsModule : IModule
    {
        public Assembly FeaturesAssembly => typeof(SubscriptionsModule).Assembly;
    }
}
