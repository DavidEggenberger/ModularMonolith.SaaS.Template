using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Kernel.Constants.Modules
{
    public partial class EndpointConstants
    {
        public static class TenantIdentity
        {
            public const string IdentityAccountPath = "/Identity/Account";
            public const string SignUpPath = "Identity/SignUp";
            public const string LoginPath = "Identity/Login";
            public const string LogoutPath = "api/user/Logout";
            public const string UserClaimsPath = "api/user";
        }
    }
}
