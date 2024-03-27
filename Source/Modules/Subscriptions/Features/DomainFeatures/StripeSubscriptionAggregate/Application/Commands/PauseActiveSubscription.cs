using Shared.Features.CQRS.Command;

namespace Modules.Subscriptions.Features.DomainFeatures.StripeSubscriptionAggregate.Application.Commands
{
    public class PauseActiveSubscription : ICommand
    {
        public Stripe.Subscription Subscription { get; set; }
    }

    public class PauseActiveSubscriptionCommandHandler : ICommandHandler<PauseActiveSubscription>
    {
        public Task HandleAsync(PauseActiveSubscription command, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
