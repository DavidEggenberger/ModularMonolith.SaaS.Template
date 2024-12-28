using Microsoft.EntityFrameworkCore;
using Shared.Features.Messaging.Command;
using Shared.Features.Server;

namespace Modules.Subscriptions.Features.DomainFeatures.StripeSubscriptions.Application.Commands
{
    public class PauseActiveSubscription : Command
    {
        public Stripe.Subscription Subscription { get; set; }
    }

    public class PauseActiveSubscriptionCommandHandler : ServerExecutionBase<SubscriptionsModule>, ICommandHandler<PauseActiveSubscription>
    {
        public PauseActiveSubscriptionCommandHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }

        public async Task HandleAsync(PauseActiveSubscription command, CancellationToken cancellationToken)
        {
            var stripeSubscription = await module.SubscriptionsDbContext.StripeSubscriptions.FirstAsync(stripeSubscription => stripeSubscription.StripePortalSubscriptionId == command.Subscription.Id);

            stripeSubscription.UpdateStatus(StripeSubscriptionStatus.Paused);

            await module.SubscriptionsDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
