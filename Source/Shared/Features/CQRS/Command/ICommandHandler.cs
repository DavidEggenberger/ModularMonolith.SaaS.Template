using Shared.Features.Server.ExecutionContext;

namespace Shared.Features.CQRS.Command
{
    public interface ICommandHandler<in TCommand> : IInServerExecutionContextScope where TCommand : ICommand
    {
        Task HandleAsync(TCommand command, CancellationToken cancellationToken);
    }
    public interface ICommandHandler<in TCommand, TResult> : IInServerExecutionContextScope where TCommand : ICommand<TResult>
    {
        Task<TResult> HandleAsync(TCommand command, CancellationToken cancellationToken);
    }
}
