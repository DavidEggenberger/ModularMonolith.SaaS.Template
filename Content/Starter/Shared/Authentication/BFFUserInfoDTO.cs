using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Authentication
{
    public class BFFUserInfoDTO
    {
        public static readonly BFFUserInfoDTO Anonymous = new BFFUserInfoDTO();
        public List<ClaimValueDTO> Claims { get; set; }
    }
}
