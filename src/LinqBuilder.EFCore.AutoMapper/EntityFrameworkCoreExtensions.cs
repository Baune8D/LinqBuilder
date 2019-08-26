using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using LinqBuilder.Core;
using Microsoft.EntityFrameworkCore;

namespace LinqBuilder.EFCore.AutoMapper
{
    public static class EntityFrameworkCoreExtensions
    {
        public static async Task<bool> AnyAsync<TEntity, T>(this IQueryable<TEntity> query, ISpecification<T> specification, IConfigurationProvider configuration)
            where TEntity : class
            where T : class
        {
            var expression = specification.Internal.QuerySpecification.AsExpression();
            return await (expression == null ? query.AnyAsync() : query.ProjectTo<T>(configuration).AnyAsync(expression));
        }

        public static async Task<bool> AllAsync<TEntity, T>(this IQueryable<TEntity> query, ISpecification<T> specification, IConfigurationProvider configuration)
            where TEntity : class
            where T : class
        {
            var expression = specification.Internal.QuerySpecification.AsExpression();
            return expression == null || await query.ProjectTo<T>(configuration).AllAsync(expression);
        }

        public static async Task<int> CountAsync<TEntity, T>(this IQueryable<TEntity> query, ISpecification<T> specification, IConfigurationProvider configuration)
            where TEntity : class
            where T : class
        {
            var expression = specification.Internal.QuerySpecification.AsExpression();
            return await (expression == null ? query.CountAsync() : query.ProjectTo<T>(configuration).CountAsync(expression));
        }

        public static async Task<T> FirstAsync<TEntity, T>(this IQueryable<TEntity> query, ISpecification<T> specification, IConfigurationProvider configuration)
            where TEntity : class
            where T : class
        {
            var expression = specification.Internal.QuerySpecification.AsExpression();
            var projectedQuery = query.ProjectTo<T>(configuration);
            return await (expression == null ? projectedQuery.FirstAsync() : projectedQuery.FirstAsync(expression));
        }

        public static async Task<T> FirstOrDefaultAsync<TEntity, T>(this IQueryable<TEntity> query, ISpecification<T> specification, IConfigurationProvider configuration)
            where TEntity : class
            where T : class
        {
            var expression = specification.Internal.QuerySpecification.AsExpression();
            var projectedQuery = query.ProjectTo<T>(configuration);
            return await (expression == null ? projectedQuery.FirstOrDefaultAsync() : projectedQuery.FirstOrDefaultAsync(expression));
        }

        public static async Task<T> SingleAsync<TEntity, T>(this IQueryable<TEntity> query, ISpecification<T> specification, IConfigurationProvider configuration)
            where TEntity : class
            where T : class
        {
            var expression = specification.Internal.QuerySpecification.AsExpression();
            var projectedQuery = query.ProjectTo<T>(configuration);
            return await (expression == null ? projectedQuery.SingleAsync() : projectedQuery.SingleAsync(expression));
        }

        public static async Task<T> SingleOrDefaultAsync<TEntity, T>(this IQueryable<TEntity> query, ISpecification<T> specification, IConfigurationProvider configuration)
            where TEntity : class
            where T : class
        {
            var expression = specification.Internal.QuerySpecification.AsExpression();
            var projectedQuery = query.ProjectTo<T>(configuration);
            return await (expression == null ? projectedQuery.SingleOrDefaultAsync() : projectedQuery.SingleOrDefaultAsync(expression));
        }
    }
}
