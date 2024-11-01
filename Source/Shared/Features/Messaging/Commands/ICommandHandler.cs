namespace Shared.Features.Messaging.Command
{
    public interface ICommandHandler<in TCommand> where TCommand : Command
    {
        Task HandleAsync(TCommand command, CancellationToken cancellationToken);
    }
    public interface ICommandHandler<in TCommand, TResult> where TCommand : Command<TResult>
    {
        Task<TResult> HandleAsync(TCommand command, CancellationToken cancellationToken);
    }
}
