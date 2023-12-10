using Shared.Kernel.BuildingBlocks;

namespace Shared.Infrastructure.DomainKernel
{ 
    public interface IExecutionContextAccessable
    {
        public IExecutionContextAccessor ExecutionContextAccessor { get; set; }
    }
}
