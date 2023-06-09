using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.LandingPages.Web.Server
{
    public static class Registrator
    {
        public static IMvcBuilder RegisterLandingPagesModulePages(this IMvcBuilder mvcBuilder)
        {
            mvcBuilder.AddApplicationPart(typeof(Registrator).Assembly);

            return mvcBuilder;
        }

        public static IEndpointRouteBuilder RegisterLandingPagesModuleFallbackPage(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.MapFallbackToPage("/LandingPage");

            return endpointRouteBuilder;
        }
    }
}
