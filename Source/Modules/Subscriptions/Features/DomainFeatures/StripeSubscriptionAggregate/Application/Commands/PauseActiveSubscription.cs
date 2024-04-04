using Microsoft.EntityFrameworkCore;
using Modules.Subscription.Features.Infrastructure.EFCore;
using Shared.Features.Messaging.Command;
using Shared.Features.Server;

namespace Modules.Subscriptions.Features.DomainFeatures.StripeSubscriptionAggregate.Application.Commands
{
    public class PauseActiveSubscription : ICommand
    {
        public Stripe.Subscription Subscription { get; set; }
    }

    public class PauseActiveSubscriptionCommandHandler : ServerExecutionBase, ICommandHandler<PauseActiveSubscription>
    {
        private readonly SubscriptionsDbContext subscriptionDbContext;

        public PauseActiveSubscriptionCommandHandler(
            SubscriptionsDbContext subscriptionDbContext,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            this.subscriptionDbContext = subscriptionDbContext;
        }

        public async Task HandleAsync(PauseActiveSubscription command, CancellationToken cancellationToken)
        {
            var stripeSubscription = await subscriptionDbContext.StripeSubscriptions.FirstAsync(stripeSubscription => stripeSubscription.StripePortalSubscriptionId == command.Subscription.Id);

            stripeSubscription.Status = StripeSubscriptionStatus.Paused;

            await subscriptionDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
