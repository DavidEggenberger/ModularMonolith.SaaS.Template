using Shared.Features.Server;

namespace Shared.Features.CQRS.Query
{
    public class BaseQueryHandler : IInServerExecutionScope
    {
        public IServerExecutionContext ExecutionContext { get; private set; }
    }
}
