using System.Collections.Generic;

namespace Modules.TenantIdentity.Public.DTOs.Tenant
{
    public class TenantExtendedDTO : TenantDTO
    {
        public List<TenantMembershipDTO> Memberships { get; set; }
        public List<TenantInvitationDTO> Invitations { get; set; }
    }
}
