using Shared.Infrastructure.CQRS.Command;
using Stripe;

namespace Modules.Subscription.DomainFeatures.Application.Commands.ApplicationSubscription
{
    public class CreateSubscription : ICommand
    {
        public Stripe.Subscription Subscription { get; set; }
    }
}
