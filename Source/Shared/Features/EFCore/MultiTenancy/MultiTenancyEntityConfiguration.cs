using Microsoft.EntityFrameworkCore;
using Shared.Features.Domain;
using System.Reflection;

namespace Shared.Features.EFCore.MultiTenancy
{
    public static class MultiTenancyEntityConfiguration
    {
        static void ConfigureEntity<TEntity, T>(ModelBuilder modelBuilder, Guid tenantId)
            where TEntity : Entity
        {
            modelBuilder.Entity<TEntity>(builder =>
            {
                builder.HasKey(x => x.Id);
                builder.Property(e => e.RowVersion).IsConcurrencyToken();
                builder.HasQueryFilter(x => x.TenantId == tenantId);
            });
        }

        public static ModelBuilder ApplyBaseEntityConfiguration(this ModelBuilder modelBuilder, Guid tenantId)
        {
            var configureEntityMethod = typeof(MultiTenancyEntityConfiguration).GetTypeInfo().DeclaredMethods
                .Single(m => m.Name == nameof(ConfigureEntity));

            foreach (var entityType in modelBuilder.Model.GetEntityTypes().Where(x => x is Entity))
            {
                configureEntityMethod.MakeGenericMethod(entityType.ClrType, entityType.GetType()).Invoke(null, new object[] { modelBuilder, tenantId });
            }

            return modelBuilder;
        }
    }
}
