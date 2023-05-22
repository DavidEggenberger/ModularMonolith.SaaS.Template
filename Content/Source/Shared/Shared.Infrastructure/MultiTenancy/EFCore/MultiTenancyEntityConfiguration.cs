using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Shared.Domain;
using Shared.Domain.Attributes;

namespace Shared.Infrastructure.MultiTenancy.EFCore
{
    public static class MultiTenancyEntityConfiguration
    {
        static void ConfigureAggregateRoot<TAggregateRoot>(ModelBuilder modelBuilder, Guid teamId)
           where TAggregateRoot : Entity
        {
            modelBuilder.Entity<TAggregateRoot>(builder =>
            {
                builder.HasQueryFilter(x => x.TenantId == teamId);
            });
        }

        static void ConfigureEntity<TEntity, T>(ModelBuilder modelBuilder)
            where TEntity : Entity
        {
            modelBuilder.Entity<TEntity>(builder =>
            {
                builder.HasKey(x => x.Id);
                builder.Property(e => e.RowVersion).IsConcurrencyToken();
            });
        }

        public static ModelBuilder ApplyBaseEntityConfiguration(this ModelBuilder modelBuilder, Guid teamId)
        {
            var configureAggregateRootMethod = typeof(MultiTenancyEntityConfiguration).GetTypeInfo().DeclaredMethods
                .Single(m => m.Name == nameof(ConfigureAggregateRoot));
            var configureEntityMethod = typeof(MultiTenancyEntityConfiguration).GetTypeInfo().DeclaredMethods
                .Single(m => m.Name == nameof(ConfigureEntity));


            foreach (var entityType in modelBuilder.Model.GetEntityTypes().Where(x => x.GetType().GetCustomAttribute(typeof(AggregateRootAttribute)) != null))
            {
                configureAggregateRootMethod.MakeGenericMethod(entityType.ClrType, entityType.GetType()).Invoke(null, new object[] { modelBuilder, teamId });
            }
            foreach (var entityType in modelBuilder.Model.GetEntityTypes().Where(x => x is Entity))
            {
                configureEntityMethod.MakeGenericMethod(entityType.ClrType, entityType.GetType()).Invoke(null, new object[] { modelBuilder });
            }

            return modelBuilder;
        }
    }
}
