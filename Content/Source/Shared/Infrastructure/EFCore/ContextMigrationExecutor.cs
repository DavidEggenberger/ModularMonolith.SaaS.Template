using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Infrastructure.EFCore
{
    public static class ContextMigrationExecutor
    {
        public static IServiceCollection MigrateContext<T>(this IServiceCollection services) where T : DbContext
        {
            using (var scope = services.BuildServiceProvider().CreateScope())
            {
                var db = scope.ServiceProvider.GetService<T>();
                db.Database.Migrate();
            }

            return services;
        }
    }
}
