using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Shared.Features.Domain;
using Shared.Kernel.BuildingBlocks;

namespace Shared.Features.EFCore
{
    public class ExecutionContextInterceptor : SaveChangesInterceptor
    {
        public object InitializedInstance(
            MaterializationInterceptionData materializationData,
            object instance)
        {
            if (instance is AggregateRoot aggregateRoot)
            {
                aggregateRoot.ExecutionContext = materializationData
                    .Context
                    .GetService<IExecutionContext>();
            }

            return instance;
        }
    }
}
