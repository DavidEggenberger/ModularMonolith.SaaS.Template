using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Features.EFCore.Configuration;
using Shared.Features.EFCore.DbUp;
using Shared.Features.Modules.Configuration;

namespace Shared.Features.EFCore
{
    public static class Registrator
    {
        public static IServiceCollection AddEFCore(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterModuleConfiguration<EFCoreConfiguration, EFCoreConfigurationValidator>(configuration);
            services.AddScoped<TransactionScopeMiddleware>();
            services.AddDbUpMigration();

            return services;
        }

        public static IApplicationBuilder UseEFCoreMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<TransactionScopeMiddleware>();

            return app;
        }
    }
}
