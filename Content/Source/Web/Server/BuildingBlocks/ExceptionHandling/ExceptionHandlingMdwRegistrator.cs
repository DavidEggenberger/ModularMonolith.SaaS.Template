using Microsoft.AspNetCore.Builder;

namespace Web.Server.BuildingBlocks.HostingInformation
{
    public static class ExceptionHandlingMdwRegistrator
    {
        public static IApplicationBuilder RegisterExceptionHandling(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseExceptionHandler("/exceptionHandler");
        }
    }
}
