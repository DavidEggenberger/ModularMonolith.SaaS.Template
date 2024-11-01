using Microsoft.Extensions.DependencyInjection;

namespace Shared.Features.Messaging.Command
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider serviceProvider;
        public CommandDispatcher(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public Task DispatchAsync<TCommand>(TCommand command, CancellationToken cancellation = default) where TCommand : Command
        {
            var handler = serviceProvider.GetRequiredService<ICommandHandler<TCommand>>();
            return handler.HandleAsync(command, cancellation);
        }
        public Task<TResult> DispatchAsync<TCommand, TResult>(TCommand command, CancellationToken cancellation = default) where TCommand : Command<TResult>
        {
            var handler = serviceProvider.GetRequiredService<ICommandHandler<TCommand, TResult>>();
            return handler.HandleAsync(command, cancellation);
        }
    }
}
