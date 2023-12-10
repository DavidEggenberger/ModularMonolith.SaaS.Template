﻿using Microsoft.Extensions.DependencyInjection;
using Shared.Kernel.BuildingBlocks.Events;

namespace Shared.DomainFeatures.CQRS.IntegrationEvent
{
    public class IntegrationEventDispatcher : IIntegrationEventDispatcher
    {
        private readonly IServiceProvider serviceProvider;

        public IntegrationEventDispatcher(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public void Raise<TIntegrationEvent>(TIntegrationEvent integrationEvent, CancellationToken cancellation = default) where TIntegrationEvent : IIntegrationEvent
        {
            var eventHandlers = serviceProvider.GetServices<IIntegrationEventHandler<TIntegrationEvent>>();
            foreach (var eventHandler in eventHandlers)
            {
                eventHandler.HandleAsync(integrationEvent, cancellation);
            }
        }
    }
}
