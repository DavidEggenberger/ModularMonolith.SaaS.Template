using Microsoft.EntityFrameworkCore;
using Shared.Features.Messaging.Command;
using Shared.Features.Server;

namespace Modules.Subscriptions.Features.DomainFeatures.StripeSubscriptionAggregate.Application.Commands
{
    public class PauseActiveSubscription : ICommand
    {
        public Stripe.Subscription Subscription { get; set; }
    }

    public class PauseActiveSubscriptionCommandHandler : ServerExecutionBase<SubscriptionsModule>, ICommandHandler<PauseActiveSubscription>
    {
        public PauseActiveSubscriptionCommandHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }

        public async Task HandleAsync(PauseActiveSubscription command, CancellationToken cancellationToken)
        {
            var stripeSubscription = await module.SubscriptionsDbContext.StripeSubscriptions.FirstAsync(stripeSubscription => stripeSubscription.StripePortalSubscriptionId == command.Subscription.Id);

            stripeSubscription.Status = StripeSubscriptionStatus.Paused;

            await module.SubscriptionsDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
