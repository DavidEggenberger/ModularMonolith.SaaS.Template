using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.TenantIdentity.Web.Server
{
    public static class Registrator
    {
        public static IMvcBuilder RegisterTenantIdentityModuleControllers(this IMvcBuilder mvcBuilder)
        {
            return mvcBuilder.AddApplicationPart(typeof(Registrator).Assembly);
        }
    }
}
