using Microsoft.Extensions.DependencyInjection;
using Shared.Features.CQRS.Command;
using Shared.Features.CQRS.DomainEvent;
using Shared.Features.CQRS.IntegrationEvent;
using Shared.Features.Server;
using Shared.Kernel.BuildingBlocks;
using Shared.Kernel.BuildingBlocks.ModelValidation;

namespace Shared.Features.CQRS.Query
{
    public class BaseQueryHandler : ServerExecutionBase
    {
        public BaseQueryHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}
