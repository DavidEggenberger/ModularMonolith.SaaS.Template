using Microsoft.EntityFrameworkCore;
using Shared.Features.DomainKernel;
using Shared.Features.DomainKernel.Attributes;
using System.Reflection;

namespace Shared.Features.EFCore.MultiTenancy
{
    public static class MultiTenancyEntityConfiguration
    {
        static void ConfigureAggregateRoot<TAggregateRoot>(ModelBuilder modelBuilder, Guid teamId)
           where TAggregateRoot : AggregateRoot
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


            foreach (var entityType in modelBuilder.Model.GetEntityTypes().Where(x => x is AggregateRoot))
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
