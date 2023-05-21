using Microsoft.AspNetCore.Builder;

namespace Web.Server.BuildingBlocks.HostingInformation
{
    public static class ExceptionHandlingMdwRegistrator
    {
        public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseExceptionHandler("/exceptionHandler");
        }
    }
}
