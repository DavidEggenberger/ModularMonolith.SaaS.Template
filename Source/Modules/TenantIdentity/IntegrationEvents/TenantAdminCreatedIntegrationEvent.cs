using Shared.Kernel.BuildingBlocks;
using System;

namespace Modules.TenantIdentity.IntegrationEvents
{
    public class TenantAdminCreatedIntegrationEvent : IIntegrationEvent
    {
        public Guid UserId { get; set; }
        public Guid TenantId { get; set; }
        public string Email { get; set; }
    }
}
