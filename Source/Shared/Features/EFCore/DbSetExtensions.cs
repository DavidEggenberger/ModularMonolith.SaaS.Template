using Microsoft.EntityFrameworkCore;
using Shared.Features.Domain;

namespace Shared.Features.EFCore
{
    public static class DbSetExtensions
    {
        public static async Task<TAggregateRoot> GetAggregateRootAsync<TAggregateRoot>(this DbSet<TAggregateRoot> dbSet, Guid owningTenantId, Guid aggregateRootId) where TAggregateRoot : AggregateRoot
        {
            return await dbSet.FirstAsync(t => t.TenantId == owningTenantId && t.Id == aggregateRootId);
        }
    }
}
