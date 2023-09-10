using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Shared.Infrastructure.DomainKernel;
using Shared.Kernel.BuildingBlocks;

namespace Shared.Infrastructure.EFCore.Interceptors
{
    public class ServiceProviderInterceptor : IMaterializationInterceptor
    {
        public object InitializedInstance(
            MaterializationInterceptionData materializationData,
            object instance)
        {
            if (instance is Entity entity)
            {
                entity.SetExecutionContextAccessor(materializationData.Context.GetService<IExecutionContextAccessor>());
            }

            return instance;
        }
    }
}
