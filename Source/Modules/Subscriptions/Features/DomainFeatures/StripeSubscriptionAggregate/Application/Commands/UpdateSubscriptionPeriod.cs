using Microsoft.EntityFrameworkCore;
using Modules.Subscription.Features.Infrastructure.EFCore;
using Shared.Features.CQRS.Command;
using Shared.Features.Server;

namespace Modules.Subscriptions.Features.DomainFeatures.StripeSubscriptionAggregate.Application.Commands
{
    public class UpdateSubscriptionPeriod : ICommand
    {
        public Stripe.Subscription Subscription { get; set; }
    }

    public class UpdateSubscriptionPerioEndCommandHandler : ServerExecutionBase, ICommandHandler<UpdateSubscriptionPeriod>
    {
        private readonly SubscriptionsDbContext subscriptionDbContext;

        public UpdateSubscriptionPerioEndCommandHandler(
            SubscriptionsDbContext subscriptionDbContext,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            this.subscriptionDbContext = subscriptionDbContext;
        }

        public async Task HandleAsync(UpdateSubscriptionPeriod command, CancellationToken cancellationToken)
        {
            var stripeSubscription = await subscriptionDbContext.StripeSubscriptions.FirstAsync(stripeSubscription => stripeSubscription.StripePortalSubscriptionId == command.Subscription.Id);

            if (stripeSubscription.Status != StripeSubscriptionStatus.Active)
            {
                stripeSubscription.Status = StripeSubscriptionStatus.Active;
            }
            stripeSubscription.ExpirationDate = command.Subscription.CurrentPeriodEnd;

            await subscriptionDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
