using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Shared.Features.Misc.Domain;
using Shared.Features.Misc.Errors;

namespace Shared.Features.EFCore
{
    public static class DbSetExtensions
    { 
        public static async Task<TEntity> GetEntityAsync<TEntity>(this DbSet<TEntity> dbSet, Guid owningTenantId, Guid entityId) where TEntity : Entity
        {
            var entity = await dbSet.FirstOrDefaultAsync(t => t.Id == entityId);
            if (entity == null)
            {
                throw Error.NotFound(typeof(TEntity).Name, entityId); 
            }

            return entity;
        }

        public static async Task<TEntity> GetEntityAsync<TEntity, TSecond>(this IIncludableQueryable<TEntity, TSecond> dbSet, Guid tenantId, Guid entityId) where TEntity : Entity
        {
            var entity = await dbSet.FirstOrDefaultAsync(t => t.Id == entityId);
            if (entity == null)
            {
                throw Error.NotFound(typeof(TEntity).Name, entityId);
            }

            return entity;
        }
    }
}
