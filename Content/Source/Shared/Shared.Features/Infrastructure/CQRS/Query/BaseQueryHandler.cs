using Shared.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;

namespace Shared.Infrastructure.CQRS.Query
{
    public class BaseQueryHandler<T, U> where T : DbContext where U : class
    {
        public DbSet<U> dbSet { get; set; }
        public BaseQueryHandler(T applicationDbContext)
        {
            dbSet = applicationDbContext.Set<U>();
        }
    }
}
