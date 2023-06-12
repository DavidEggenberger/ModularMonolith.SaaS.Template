using Shared.Infrastructure.CQRS.Command;
using Stripe;

namespace Modules.Subscription.DomainFeatures.Application.Commands
{
    public class UpdateSubscription : ICommand
    {
        public Subscription Subscription { get; set; }
    }
}
