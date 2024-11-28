using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Shared.Features.Domain;
using Shared.Kernel.Errors;

namespace Shared.Features.EFCore
{
    public static class DbSetExtensions
    { 
        public static async Task<TEntity> GetEntityAsync<TEntity>(this DbSet<TEntity> dbSet, Guid owningTenantId, Guid entityId) where TEntity : Entity
        {
            var entity = await dbSet.FirstOrDefaultAsync(t => t.Id == entityId);
            if (entity == null)
            {
                throw Errors.NotFound(typeof(TEntity).Name, entityId); 
            }
            if (entity.TenantId != owningTenantId)
            {
                throw Errors.UnAuthorized;
            }

            return entity;
        }

        public static async Task<TEntity> GetEntityAsync<TEntity, TSecond>(this IIncludableQueryable<TEntity, TSecond> dbSet, Guid tenantId, Guid entityId) where TEntity : Entity
        {
            var entity = await dbSet.FirstOrDefaultAsync(t => t.Id == entityId);
            if (entity == null)
            {
                throw Errors.NotFound(typeof(TEntity).Name, entityId);
            }
            if (entity.TenantId != tenantId)
            {
                throw Errors.UnAuthorized;
            }

            return entity;
        }
    }
}
