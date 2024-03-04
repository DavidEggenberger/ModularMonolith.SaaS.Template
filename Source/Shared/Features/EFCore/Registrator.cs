using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Shared.Features.EFCore.Configuration;
using Shared.Features.Server.ExecutionContext;

namespace Shared.Features.EFCore
{
    public static class Registrator
    {
        public static IServiceCollection RegisterEFCore(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EFCoreConfiguration>(configuration.GetSection(nameof(EFCoreConfiguration)));
            services.AddScoped(sp => sp.GetRequiredService<IOptions<EFCoreConfiguration>>().Value);
            services.AddSingleton<IValidateOptions<EFCoreConfiguration>, EFCoreConfigurationValidator>();

            return services;
        }

        public static IApplicationBuilder UseEFCoreMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ServerExecutionContextMiddleware>();

            return app;
        }
    }
}
