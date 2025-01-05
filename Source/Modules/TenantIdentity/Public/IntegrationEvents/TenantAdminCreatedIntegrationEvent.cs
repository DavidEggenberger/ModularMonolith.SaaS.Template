using Shared.Kernel.BuildingBlocks.IntegrationEvents;
using System;

namespace Modules.TenantIdentity.Public.IntegrationEvents
{
    public class TenantAdminCreatedIntegrationEvent : IIntegrationEvent
    {
        public Guid UserId { get; set; }
        public Guid TenantId { get; set; }
        public string Email { get; set; }
    }
}
