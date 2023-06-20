using Microsoft.AspNetCore.Builder;

namespace WebServer.Modules.Swagger
{
    public static class SwaggerMdwRegistrator
    {
        public static IApplicationBuilder RegisterSwagger(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseSwagger();
            applicationBuilder.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = "api/swagger/index.html";
            });

            return applicationBuilder;
        }
    }
}
