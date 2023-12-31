using Shared.Kernel.BuildingBlocks;

namespace Shared.Features.DomainKernel
{ 
    public interface IExecutionContextAccessable
    {
        public IExecutionContextAccessor ExecutionContextAccessor { get; set; }
    }
}
