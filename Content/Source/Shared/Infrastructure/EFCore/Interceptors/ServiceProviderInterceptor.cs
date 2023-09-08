using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Shared.Infrastructure.DomainKernel;

namespace Shared.Infrastructure.EFCore.Interceptors
{
    public class ServiceProviderInterceptor : IMaterializationInterceptor
    {
        public object InitializedInstance(
            MaterializationInterceptionData materializationData,
            object instance)
        {
            if (instance is IExecutionContextAccessable entity)
            {
                entity.ExecutionContextAccessor = materializationData.Context.GetService<IExecutionContextAccessor>();
            }

            return instance;
        }
    }
}
