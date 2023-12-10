using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.DomainFeatures.EmailSender.Services;

namespace Shared.DomainFeatures.EmailSender
{
    public static class EmailSenderDIRegistrator
    {
        public static IServiceCollection RegisterEmailSender(this IServiceCollection services, IConfiguration configuration)
        {
            //services.Configure<SendGridEmailOptions>(configuration);
            services.AddTransient<IEmailSender, SendGridEmailSender>();

            return services;
        }
    }
}
