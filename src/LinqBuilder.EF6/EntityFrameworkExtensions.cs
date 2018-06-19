using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using LinqBuilder.Core;

namespace LinqBuilder.EF6
{
    public static class EntityFrameworkExtensions
    {
        public static async Task<bool> AnyAsync<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification)
            where TEntity : class
        {
            return await query.AnyAsync(specification.GetLinqBuilder().QuerySpecification.AsExpression());
        }

        public static async Task<bool> AllAsync<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification)
            where TEntity : class
        {
            return await query.AllAsync(specification.GetLinqBuilder().QuerySpecification.AsExpression());
        }

        public static async Task<int> CountAsync<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification)
            where TEntity : class
        {
            return await query.CountAsync(specification.GetLinqBuilder().QuerySpecification.AsExpression());
        }

        public static async Task<TEntity> FirstAsync<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification)
            where TEntity : class
        {
            return await query.FirstAsync(specification.GetLinqBuilder().QuerySpecification.AsExpression());
        }

        public static async Task<TEntity> FirstOrDefaultAsync<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification)
            where TEntity : class
        {
            return await query.FirstOrDefaultAsync(specification.GetLinqBuilder().QuerySpecification.AsExpression());
        }

        public static async Task<TEntity> SingleAsync<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification)
            where TEntity : class
        {
            return await query.SingleAsync(specification.GetLinqBuilder().QuerySpecification.AsExpression());
        }

        public static async Task<TEntity> SingleOrDefaultAsync<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification)
            where TEntity : class
        {
            return await query.SingleOrDefaultAsync(specification.GetLinqBuilder().QuerySpecification.AsExpression());
        }
    }
}
