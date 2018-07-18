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
            var expression = specification.Internal.QuerySpecification.AsExpression();
            return await (expression == null ? query.AnyAsync() : query.AnyAsync(expression));
        }

        public static async Task<bool> AllAsync<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification)
            where TEntity : class
        {
            var expression = specification.Internal.QuerySpecification.AsExpression();
            return expression == null || await query.AllAsync(expression);
        }

        public static async Task<int> CountAsync<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification)
            where TEntity : class
        {
            var expression = specification.Internal.QuerySpecification.AsExpression();
            return await (expression == null ? query.CountAsync() : query.CountAsync(expression));
        }

        public static async Task<TEntity> FirstAsync<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification)
            where TEntity : class
        {
            var expression = specification.Internal.QuerySpecification.AsExpression();
            return await (expression == null ? query.FirstAsync() : query.FirstAsync(expression));
        }

        public static async Task<TEntity> FirstOrDefaultAsync<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification)
            where TEntity : class
        {
            var expression = specification.Internal.QuerySpecification.AsExpression();
            return await (expression == null ? query.FirstOrDefaultAsync() : query.FirstOrDefaultAsync(expression));
        }

        public static async Task<TEntity> SingleAsync<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification)
            where TEntity : class
        {
            var expression = specification.Internal.QuerySpecification.AsExpression();
            return await (expression == null ? query.SingleAsync() : query.SingleAsync(expression));
        }

        public static async Task<TEntity> SingleOrDefaultAsync<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification)
            where TEntity : class
        {
            var expression = specification.Internal.QuerySpecification.AsExpression();
            return await (expression == null ? query.SingleOrDefaultAsync() : query.SingleOrDefaultAsync(expression));
        }
    }
}
