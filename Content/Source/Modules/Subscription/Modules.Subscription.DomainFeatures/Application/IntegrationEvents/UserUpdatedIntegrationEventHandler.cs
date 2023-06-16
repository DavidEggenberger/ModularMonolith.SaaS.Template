using Modules.TenantIdentity.IntegrationEvents;
using Shared.Infrastructure.CQRS.Command;

namespace Modules.Subscription.DomainFeatures.Application.IntegrationEvents
{
    public class UserUpdatedIntegrationEventHandler : ICommandHandler<UserCreatedIntegrationEvent>
    {
        public Task HandleAsync(UserCreatedIntegrationEvent command, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
