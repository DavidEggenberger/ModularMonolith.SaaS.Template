using Microsoft.Extensions.DependencyInjection;
using Shared.Features.Messaging.Commands;
using Shared.Features.Messaging.IntegrationEvents;
using Shared.Features.Messaging.Queries;
using Shared.Features.Modules;
using Shared.Features.Server.ExecutionContext;
using Shared.Features.SignalR;
using Shared.Kernel.BuildingBlocks.Services.ModelValidation;

namespace Shared.Features.Server
{
    public class ServerExecutionBase<TModule> : ServerExecutionBase where TModule : IModule
    {
        protected readonly TModule module;

        public ServerExecutionBase(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            module = serviceProvider.GetRequiredService<TModule>();
        }
    }

    public class ServerExecutionBase
    {
        protected readonly IExecutionContext executionContext;
        protected readonly ICommandDispatcher commandDispatcher;
        protected readonly IQueryDispatcher queryDispatcher;
        protected readonly IIntegrationEventDispatcher integrationEventDispatcher;
        protected readonly IValidationService validationService;
        protected readonly INotificationHubService notificationHubService;

        public ServerExecutionBase(IServiceProvider serviceProvider)
        {
            executionContext = serviceProvider.GetRequiredService<IExecutionContext>();
            commandDispatcher = serviceProvider.GetRequiredService<ICommandDispatcher>();
            queryDispatcher = serviceProvider.GetRequiredService<IQueryDispatcher>();
            integrationEventDispatcher = serviceProvider.GetRequiredService<IIntegrationEventDispatcher>();
            validationService = serviceProvider.GetRequiredService<IValidationService>();
            notificationHubService = serviceProvider.GetRequiredService<INotificationHubService>();
        }
    }
}
