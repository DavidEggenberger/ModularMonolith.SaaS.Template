using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;
using Shared.Features.Messaging.Commands;
using Shared.Features.Messaging.Queries;
using Shared.Features.Messaging.IntegrationMessages;

namespace Shared.Features.Messaging
{
    public static class Registrator
    {
        public static IServiceCollection AddMessaging(this IServiceCollection services)
        {
            services.TryAddScoped<ICommandDispatcher, CommandDispatcher>();
            services.TryAddScoped<IQueryDispatcher, QueryDispatcher>();
            services.TryAddScoped<IIntegrationMessageDispatcher, IntegrationMessageDispatcher>();

            return services;
        }

        public static IServiceCollection AddMessagingForModule(this IServiceCollection services, Type moduleType)
        {
            var assemblies = new Assembly[] { moduleType.Assembly };

            // INFO: Using https://www.nuget.org/packages/Scrutor for registering all Query and Command handlers by convention
            services.Scan(selector =>
            {
                selector.FromAssemblies(assemblies)
                        .AddClasses(filter =>
                        {
                            filter.AssignableTo(typeof(IQueryHandler<,>));
                        })
                        .AsImplementedInterfaces()
                        .WithScopedLifetime();
            });
            services.Scan(selector =>
            {
                selector.FromAssemblies(assemblies)
                        .AddClasses(filter =>
                        {
                            filter.AssignableTo(typeof(ICommandHandler<>));
                        })
                        .AsImplementedInterfaces()
                        .WithScopedLifetime();

                selector.FromAssemblies(assemblies)
                        .AddClasses(filter =>
                        {
                            filter.AssignableTo(typeof(ICommandHandler<,>));
                        })
                        .AsImplementedInterfaces()
                        .WithScopedLifetime();
            });
            services.Scan(selector =>
            {
                selector.FromAssemblies(assemblies)
                        .AddClasses(filter =>
                        {
                            filter.AssignableTo(typeof(IIntegrationEventHandler<>));
                        })
                        .AsImplementedInterfaces()
                        .WithScopedLifetime();
            });
            services.Scan(selector =>
            {
                selector.FromAssemblies(assemblies)
                        .AddClasses(filter =>
                        {
                            filter.AssignableTo(typeof(IIntegrationRequestHandler<,>));
                        })
                        .AsImplementedInterfaces()
                        .WithScopedLifetime();
            });

            return services;
        }
    }
}
