using Shared.Kernel.BuildingBlocks.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.TenantIdentity.IntegrationEvents
{
    public class UserEmailUpdatedIntegrationEvent : IIntegrationEvent
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
    }
}
