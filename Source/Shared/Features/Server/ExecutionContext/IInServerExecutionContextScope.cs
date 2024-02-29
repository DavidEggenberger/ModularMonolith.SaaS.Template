using Shared.Kernel.BuildingBlocks;

namespace Shared.Features.Server.ExecutionContext
{
    public interface IInServerExecutionContextScope
    {
        public IExecutionContext ExecutionContext { get; init; }
    }
}
