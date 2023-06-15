using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.TenantIdentity.IntegrationEvents
{
    public class UserCreatedIntegrationEvent
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
    }
}
