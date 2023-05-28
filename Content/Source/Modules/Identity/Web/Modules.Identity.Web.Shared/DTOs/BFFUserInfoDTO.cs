using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.TenantIdentity.Web.Shared.DTOs
{
    public class BFFUserInfoDTO
    {
        public List<ClaimValueDTO> Claims { get; set; }
    }
}
