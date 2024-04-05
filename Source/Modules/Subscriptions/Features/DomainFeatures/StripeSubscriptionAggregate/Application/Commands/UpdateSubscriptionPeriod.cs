using Microsoft.EntityFrameworkCore;
using Shared.Features.Messaging.Command;
using Shared.Features.Server;

namespace Modules.Subscriptions.Features.DomainFeatures.StripeSubscriptionAggregate.Application.Commands
{
    public class UpdateSubscriptionPeriod : ICommand
    {
        public Stripe.Subscription Subscription { get; set; }
    }

    public class UpdateSubscriptionPerioEndCommandHandler : ServerExecutionBase<SubscriptionsModule>, ICommandHandler<UpdateSubscriptionPeriod>
    {
        public UpdateSubscriptionPerioEndCommandHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }

        public async Task HandleAsync(UpdateSubscriptionPeriod command, CancellationToken cancellationToken)
        {
            var stripeSubscription = await module.SubscriptionsDbContext.StripeSubscriptions.FirstAsync(stripeSubscription => stripeSubscription.StripePortalSubscriptionId == command.Subscription.Id);

            if (stripeSubscription.Status != StripeSubscriptionStatus.Active)
            {
                stripeSubscription.Status = StripeSubscriptionStatus.Active;
            }
            stripeSubscription.ExpirationDate = command.Subscription.CurrentPeriodEnd;

            await module.SubscriptionsDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
