using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Modules.TenantIdentity.Features.Domain.TenantAggregate;
using Shared.Kernel.BuildingBlocks.Auth;
using Shared.Kernel.BuildingBlocks.ContextAccessors;
using System;

namespace Web.Server.BuildingBlocks.ContextAccessor.ExecutionContextAccessor
{
    public static class Registrator
    {
        public static IServiceCollection RegisterExecutionContextAccessor(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<IExecutionContext, ExecutionContext>(serviceProvider =>
            {
                var executionContext = new ExecutionContext();
                executionContext.HostingEnvironment = serviceProvider.GetRequiredService<IHostEnvironment>();

                return executionContext;
            });
            return services;
        }

        public static IApplicationBuilder RegisterExecutionContextAccessingMiddleware(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.Use(async (context, next) =>
            {
                var hostEnvironment = context.RequestServices.GetService<IHostEnvironment>();
                executionContext.UserId = capturedHttpContext.User.GetUserId<Guid>(),
                TenantId = capturedHttpContext.User.GetTenantId<Guid>(),
                TenantPlan = capturedHttpContext.User.GetTenantSubscriptionPlanType(),
                TenantRole = capturedHttpContext.User.GetTenantRole()

                await next(context);
            });

            return applicationBuilder;
        }
    }
}
