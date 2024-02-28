using Shared.Kernel.BuildingBlocks;

namespace Shared.Features.Server.ExecutionContext
{
    public interface IInServerExecutionScope
    {
        public IExecutionContext ExecutionContext { get; init; }
    }
}
