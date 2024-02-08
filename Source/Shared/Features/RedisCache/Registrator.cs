using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Features.Domain.Infrastructure.RedisCache
{
    public static class Registrator
    {
        public static IServiceCollection RegisterRedisCache(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            //var redisCacheConfiguration = configuration.GetSection("Redis").Get<RedisCacheConfiguration>();

            ////registers Redis as IDistributedCache
            //return serviceCollection.AddStackExchangeRedisCache(options =>
            //{
            //    options.Configuration = $"{redisCacheConfiguration.ConnectionString},ssl=True,password={redisCacheConfiguration.Password},abortConnect=false,connectTimeout=30000,responseTimeout=30000";
            //    options.InstanceName = "RedisCache-CrispyCollab";
            //});
            return serviceCollection;
        }
    }
}
