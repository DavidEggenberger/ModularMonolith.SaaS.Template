using Shared.Features.CQRS.Command;

namespace Modules.Subscriptions.Features.DomainFeatures.StripeSubscriptionAggregate.Application.Commands
{
    public class UpdateSubscriptionPeriod : ICommand
    {
        public Stripe.Subscription Subscription { get; set; }
    }

    public class UpdateSubscriptionPerioEndCommandHandler : ICommandHandler<UpdateSubscriptionPeriod>
    {
        public Task HandleAsync(UpdateSubscriptionPeriod command, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
