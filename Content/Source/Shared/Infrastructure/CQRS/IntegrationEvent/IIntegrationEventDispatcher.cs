﻿using Shared.DomainFeatures.DomainKernel.Events;
using Shared.Infrastructure.CQRS.Command;

namespace Shared.Infrastructure.CQRS.IntegrationEvent
{
    public interface IIntegrationEventDispatcher
    {
        void Raise<TIntegrationEvent>(TIntegrationEvent command, CancellationToken cancellation) where TIntegrationEvent : IIntegrationEvent;

        //Task<TResult> RaiseAndWaitForResultAsync<TIntegrationEvent, TResult>(TIntegrationEvent command, CancellationToken cancellation = default) where TIntegrationEvent : IIntegrationEvent<TResult>;
    }
}