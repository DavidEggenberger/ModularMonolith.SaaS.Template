using Shared.Infrastructure.CQRS.Command;
using Shared.Infrastructure.DomainKernel.Events;
using System;

namespace Modules.TenantIdentity.IntegrationEvents
{
    public class UserCreatedIntegrationEvent : IIntegrationEvent
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
    }
}
