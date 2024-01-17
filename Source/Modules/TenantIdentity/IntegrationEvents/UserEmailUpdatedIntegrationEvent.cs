using Shared.Kernel.BuildingBlocks.Events;
using System;

namespace Modules.TenantIdentity.IntegrationEvents
{
    public class UserEmailUpdatedIntegrationEvent : IIntegrationEvent
    {
        public Guid UserId { get; set; }
        public Guid TenantId { get; set; }
        public string Email { get; set; }
    }
}
