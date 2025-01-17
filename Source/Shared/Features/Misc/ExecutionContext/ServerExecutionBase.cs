using Microsoft.Extensions.DependencyInjection;
using Shared.Features.Messaging.Commands;
using Shared.Features.Messaging.IntegrationMessages;
using Shared.Features.Messaging.Queries;
using Shared.Features.Misc.Modules;
using Shared.Features.SignalR;
using Shared.Kernel.BuildingBlocks.Services.ModelValidation;

namespace Shared.Features.Misc.ExecutionContext
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
        protected readonly IIntegrationMessageDispatcher integrationMessageDispatcher;
        protected readonly IValidationService validationService;
        protected readonly INotificationHubService notificationHubService;

        public ServerExecutionBase(IServiceProvider serviceProvider)
        {
            executionContext = serviceProvider.GetRequiredService<IExecutionContext>();
            commandDispatcher = serviceProvider.GetRequiredService<ICommandDispatcher>();
            queryDispatcher = serviceProvider.GetRequiredService<IQueryDispatcher>();
            integrationMessageDispatcher = serviceProvider.GetRequiredService<IIntegrationMessageDispatcher>();
            validationService = serviceProvider.GetRequiredService<IValidationService>();
            notificationHubService = serviceProvider.GetRequiredService<INotificationHubService>();
        }
    }
}
