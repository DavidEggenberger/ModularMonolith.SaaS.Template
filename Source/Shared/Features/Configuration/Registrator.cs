using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Shared.Features.Configuration
{
    public static class Registrator
    {
        public static IServiceCollection RegisterConfiguration<TConfiguration>(this IServiceCollection services, IConfiguration configuration) where TConfiguration : ConfigurationObject, new()
        {
            services.AddScoped(sp =>
            {
                TConfiguration tc = new TConfiguration();
                configuration.GetSection(typeof(TConfiguration).Name).Bind(tc);
                return tc;
            });
            services.Configure<TConfiguration>(configuration.GetSection(nameof(TConfiguration)));

            return services;
        }

        public static IServiceCollection RegisterConfiguration<TConfiguration, TConfigurationValidator>(this IServiceCollection services, IConfiguration configuration) 
            where TConfiguration : ConfigurationObject, new() 
            where TConfigurationValidator : ConfigurationObjectValidator<TConfiguration>, new()
        {
            services.AddScoped(sp =>
            {
                TConfiguration tc = new TConfiguration();
                configuration.GetSection(typeof(TConfiguration).Name).Bind(tc);
                return tc;
            });
            services.Configure<TConfiguration>(configuration.GetSection(nameof(TConfiguration)));
            services.AddSingleton<IValidateOptions<TConfiguration>, TConfigurationValidator>();

            return services;
        }
    }
}
