using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Shared.Features.DomainKernel;
using Shared.Kernel.BuildingBlocks.ContextAccessors;

namespace Shared.Features.EFCore
{
    public class ExecutionContextInterceptor : IMaterializationInterceptor
    {
        public object InitializedInstance(
            MaterializationInterceptionData materializationData,
            object instance)
        {
            if (instance is AggregateRoot aggregateRoot)
            {
                aggregateRoot.ExecutionContext = materializationData
                    .Context
                    .GetService<IExecutionContextAccessor>().ExecutionContext;
            }

            return instance;
        }
    }
}
