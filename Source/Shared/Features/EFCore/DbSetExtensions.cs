using Microsoft.EntityFrameworkCore;
using Shared.Features.Domain;

namespace Shared.Features.EFCore
{
    public static class DbSetExtensions
    {
        public static async Task<TEntity> GetEntityAsync<TEntity>(this DbSet<TEntity> dbSet, Guid owningTenantId, Guid entityId) where TEntity : Entity
        {
            return await dbSet.FirstAsync(t => t.TenantId == owningTenantId && t.Id == entityId);
        }
    }
}
