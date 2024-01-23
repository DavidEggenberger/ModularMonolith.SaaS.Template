namespace Shared.Kernel.BuildingBlocks.ContextAccessors
{
    public interface IExecutionContextAccessor
    {
        IExecutionContext ExecutionContext { get; }
    }
}
