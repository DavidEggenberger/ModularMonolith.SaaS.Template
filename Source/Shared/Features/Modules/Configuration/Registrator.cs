using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Shared.Features.Modules.Configuration
{
    public static class Registrator
    {
        public static IServiceCollection RegisterModuleConfiguration<TModuleConfiguration>(this IServiceCollection services, IConfiguration configuration) where TModuleConfiguration : class, IModuleConfiguration, new()
        {
            services.Configure<TModuleConfiguration>(configuration.GetSection(nameof(TModuleConfiguration)));
            services.AddScoped<TModuleConfiguration>(sp =>
            {
                TModuleConfiguration sc = new TModuleConfiguration();
                sp.GetRequiredService<IConfiguration>().GetSection(typeof(TModuleConfiguration).Name).Bind(sc);
                return sc;
            });

            return services;
        }

        public static IServiceCollection RegisterModuleConfiguration<TModuleConfiguration, TModuleConfigurationValidator>(this IServiceCollection services, IConfiguration configuration) where TModuleConfiguration : class, IModuleConfiguration, new() where TModuleConfigurationValidator : class, IValidateOptions<TModuleConfiguration>
        {
            services.Configure<TModuleConfiguration>(configuration.GetSection(nameof(TModuleConfiguration)));
            services.AddScoped<TModuleConfiguration>(sp =>
            {
                TModuleConfiguration sc = new TModuleConfiguration();
                sp.GetRequiredService<IConfiguration>().GetSection(typeof(TModuleConfiguration).Name).Bind(sc);
                return sc;
            });

            services.AddSingleton<IValidateOptions<TModuleConfiguration>, TModuleConfigurationValidator>();

            return services;
        }
    }
}
