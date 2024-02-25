using Shared.Features.CQRS.Command;

namespace Modules.Subscriptions.DomainFeatures.StripeSubscriptionAggregate.Application.Commands
{
    public class UpdateSubscriptionPeriodEnd : ICommand
    {
        public Stripe.Subscription Subscription { get; set; }
    }

    public class UpdateSubscriptionPerioEndCommandHandler : ICommandHandler<UpdateSubscriptionPeriodEnd>
    {
        public Task HandleAsync(UpdateSubscriptionPeriodEnd command, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
