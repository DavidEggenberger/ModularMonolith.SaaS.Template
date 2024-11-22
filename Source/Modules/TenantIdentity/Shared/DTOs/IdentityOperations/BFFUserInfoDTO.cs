using System.Collections.Generic;

namespace Modules.TenantIdentity.Shared.DTOs.IdentityOperations
{
    public class BFFUserInfoDTO
    {
        public List<ClaimValueDTO> Claims { get; set; }
        public static readonly BFFUserInfoDTO Anonymous = new BFFUserInfoDTO();
    }
}
