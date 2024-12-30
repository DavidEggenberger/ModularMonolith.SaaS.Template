using Microsoft.EntityFrameworkCore.Metadata;
using Shared.Features.Modules;
using Shared.Features.Server;

namespace Shared.Features.Messaging.Commands
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
