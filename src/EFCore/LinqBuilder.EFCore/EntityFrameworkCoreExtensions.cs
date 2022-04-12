using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LinqBuilder.EFCore
{
    public static class EntityFrameworkCoreExtensions
    {
        public static async Task<bool> AnyAsync<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification)
            where TEntity : class
        {
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            var expression = specification.Internal.QuerySpecification.AsExpression();
            return await (expression == null ? query.AnyAsync() : query.AnyAsync(expression)).ConfigureAwait(false);
        }

        public static async Task<bool> AllAsync<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification)
            where TEntity : class
        {
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            var expression = specification.Internal.QuerySpecification.AsExpression();
            return expression == null || await query.AllAsync(expression).ConfigureAwait(false);
        }

        public static async Task<int> CountAsync<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification)
            where TEntity : class
        {
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            var expression = specification.Internal.QuerySpecification.AsExpression();
            return await (expression == null ? query.CountAsync() : query.CountAsync(expression)).ConfigureAwait(false);
        }

        public static async Task<TEntity> FirstAsync<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification)
            where TEntity : class
        {
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            var expression = specification.Internal.QuerySpecification.AsExpression();
            return await (expression == null ? query.FirstAsync() : query.FirstAsync(expression)).ConfigureAwait(false);
        }

        public static async Task<TEntity?> FirstOrDefaultAsync<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification)
            where TEntity : class
        {
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            var expression = specification.Internal.QuerySpecification.AsExpression();
            return await (expression == null ? query.FirstOrDefaultAsync() : query.FirstOrDefaultAsync(expression)).ConfigureAwait(false);
        }

        public static async Task<TEntity> SingleAsync<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification)
            where TEntity : class
        {
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            var expression = specification.Internal.QuerySpecification.AsExpression();
            return await (expression == null ? query.SingleAsync() : query.SingleAsync(expression)).ConfigureAwait(false);
        }

        public static async Task<TEntity?> SingleOrDefaultAsync<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification)
            where TEntity : class
        {
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            var expression = specification.Internal.QuerySpecification.AsExpression();
            return await (expression == null ? query.SingleOrDefaultAsync() : query.SingleOrDefaultAsync(expression)).ConfigureAwait(false);
        }
    }
}
