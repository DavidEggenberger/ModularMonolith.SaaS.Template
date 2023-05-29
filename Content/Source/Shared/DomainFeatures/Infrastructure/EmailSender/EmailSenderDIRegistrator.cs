using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.DomainFeatures.Infrastructure.EmailSender.Services;

namespace Shared.DomainFeatures.Infrastructure.EmailSender
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
