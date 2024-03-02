using Shared.Features.Server;

namespace Shared.Features.CQRS.Command
{
    public interface ICommandHandler<in TCommand> : ServerExecutionBase where TCommand : ICommand
    {
        Task HandleAsync(TCommand command, CancellationToken cancellationToken);
    }
    public interface ICommandHandler<in TCommand, TResult> : ServerExecutionBase where TCommand : ICommand<TResult>
    {
        Task<TResult> HandleAsync(TCommand command, CancellationToken cancellationToken);
    }
}
