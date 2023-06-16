using Shared.Infrastructure.CQRS.Command;
using System;

namespace Modules.TenantIdentity.IntegrationEvents
{
    public class UserCreatedIntegrationEvent : ICommand
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
    }
}
