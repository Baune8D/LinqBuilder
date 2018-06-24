using System.Linq;
using System.Threading.Tasks;
using LinqBuilder.Core;
using Microsoft.EntityFrameworkCore;

namespace LinqBuilder.EFCore
{
    public static class EntityFrameworkCoreExtensions
    {
        public static async Task<bool> AnyAsync<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification)
            where TEntity : class
        {
            return await query.AnyAsync(specification.Internal.QuerySpecification.AsExpression());
        }

        public static async Task<bool> AllAsync<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification)
            where TEntity : class
        {
            return await query.AllAsync(specification.Internal.QuerySpecification.AsExpression());
        }

        public static async Task<int> CountAsync<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification)
            where TEntity : class
        {
            return await query.CountAsync(specification.Internal.QuerySpecification.AsExpression());
        }

        public static async Task<TEntity> FirstAsync<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification)
            where TEntity : class
        {
            return await query.FirstAsync(specification.Internal.QuerySpecification.AsExpression());
        }

        public static async Task<TEntity> FirstOrDefaultAsync<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification)
            where TEntity : class
        {
            return await query.FirstOrDefaultAsync(specification.Internal.QuerySpecification.AsExpression());
        }

        public static async Task<TEntity> SingleAsync<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification)
            where TEntity : class
        {
            return await query.SingleAsync(specification.Internal.QuerySpecification.AsExpression());
        }

        public static async Task<TEntity> SingleOrDefaultAsync<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification)
            where TEntity : class
        {
            return await query.SingleOrDefaultAsync(specification.Internal.QuerySpecification.AsExpression());
        }
    }
}
