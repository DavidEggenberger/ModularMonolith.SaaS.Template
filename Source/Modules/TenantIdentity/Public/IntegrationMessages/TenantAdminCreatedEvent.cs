using Shared.Kernel.BuildingBlocks;
using System;

namespace Modules.TenantIdentity.Public.IntegrationMessages
{
    public class TenantAdminCreatedEvent : IIntegrationEvent
    {
        public Guid UserId { get; set; }
        public Guid TenantId { get; set; }
        public string Email { get; set; }
    }
}
