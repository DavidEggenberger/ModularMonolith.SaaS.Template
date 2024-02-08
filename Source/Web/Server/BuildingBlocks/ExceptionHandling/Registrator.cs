using Microsoft.AspNetCore.Builder;

namespace Web.Server.BuildingBlocks.ExceptionHandling
{
    public static class Registrator
    {
        public static IApplicationBuilder RegisterExceptionHandling(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseExceptionHandler("/exceptionHandler");
        }
    }
}
