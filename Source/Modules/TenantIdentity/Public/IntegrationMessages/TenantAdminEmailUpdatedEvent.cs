﻿using Shared.Kernel.BuildingBlocks;
using System;

namespace Modules.TenantIdentity.Public.IntegrationMessages
{
    public class TenantAdminEmailUpdatedEvent : IIntegrationEvent
    {
        public Guid UserId { get; set; }
        public Guid TenantId { get; set; }
        public string Email { get; set; }
    }
}
