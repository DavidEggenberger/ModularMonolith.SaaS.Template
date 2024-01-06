using Modules.Subscription.Features.Infrastructure.EFCore;
using Shared.Features.CQRS.Command;
using System.Threading;
using System.Threading.Tasks;

namespace Modules.Subscriptions.Features.Aggregates.StripeSubscriptionAggregate.Application.Commands
{
    public class UpdateActiveSubscription : ICommand
    {
        public string StripeCustomerId { get; set; }
        public Stripe.Subscription Subscription { get; set; }
    }

    public class UpdateSubscriptionCommandHandler : ICommandHandler<UpdateActiveSubscription>
    {
        private readonly SubscriptionsDbContext subscriptionDbContext;

        public UpdateSubscriptionCommandHandler(SubscriptionsDbContext subscriptionDbContext)
        {
            this.subscriptionDbContext = subscriptionDbContext;
        }

        public async Task HandleAsync(UpdateActiveSubscription command, CancellationToken cancellationToken)
        {

        }
    }
}
