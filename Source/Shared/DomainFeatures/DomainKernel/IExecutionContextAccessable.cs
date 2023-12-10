using Shared.Kernel.BuildingBlocks;

namespace Shared.DomainFeatures.DomainKernel
{ 
    public interface IExecutionContextAccessable
    {
        public IExecutionContextAccessor ExecutionContextAccessor { get; set; }
    }
}
