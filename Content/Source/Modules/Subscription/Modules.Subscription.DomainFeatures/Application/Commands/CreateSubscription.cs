using Shared.Infrastructure.CQRS.Command;

namespace Modules.Subscription.DomainFeatures.Application.Commands
{
    public class CreateSubscription : ICommand
    {
        public Subscription Subscription { get; set; }
    }
}
