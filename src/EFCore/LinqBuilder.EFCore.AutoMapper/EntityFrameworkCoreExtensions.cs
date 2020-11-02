using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DelegateDecompiler.EntityFrameworkCore;
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
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            var expression = specification.Internal.QuerySpecification.AsExpression();
            return await (expression == null ? query.AnyAsync() : query.ProjectTo<T>(configuration).DecompileAsync().AnyAsync(expression)).ConfigureAwait(false);
        }

        public static async Task<bool> AllAsync<TEntity, T>(this IQueryable<TEntity> query, ISpecification<T> specification, IConfigurationProvider configuration)
            where TEntity : class
            where T : class
        {
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            var expression = specification.Internal.QuerySpecification.AsExpression();
            return expression == null || await query.ProjectTo<T>(configuration).DecompileAsync().AllAsync(expression).ConfigureAwait(false);
        }

        public static async Task<int> CountAsync<TEntity, T>(this IQueryable<TEntity> query, ISpecification<T> specification, IConfigurationProvider configuration)
            where TEntity : class
            where T : class
        {
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            var expression = specification.Internal.QuerySpecification.AsExpression();
            return await (expression == null ? query.CountAsync() : query.ProjectTo<T>(configuration).DecompileAsync().CountAsync(expression)).ConfigureAwait(false);
        }

        public static async Task<T> FirstAsync<TEntity, T>(this IQueryable<TEntity> query, ISpecification<T> specification, IConfigurationProvider configuration)
            where TEntity : class
            where T : class
        {
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            var expression = specification.Internal.QuerySpecification.AsExpression();
            var projectedQuery = query.ProjectTo<T>(configuration).DecompileAsync();
            return await (expression == null ? projectedQuery.FirstAsync() : projectedQuery.FirstAsync(expression)).ConfigureAwait(false);
        }

        public static async Task<T> FirstOrDefaultAsync<TEntity, T>(this IQueryable<TEntity> query, ISpecification<T> specification, IConfigurationProvider configuration)
            where TEntity : class
            where T : class
        {
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            var expression = specification.Internal.QuerySpecification.AsExpression();
            var projectedQuery = query.ProjectTo<T>(configuration).DecompileAsync();
            return await (expression == null ? projectedQuery.FirstOrDefaultAsync() : projectedQuery.FirstOrDefaultAsync(expression)).ConfigureAwait(false);
        }

        public static async Task<T> SingleAsync<TEntity, T>(this IQueryable<TEntity> query, ISpecification<T> specification, IConfigurationProvider configuration)
            where TEntity : class
            where T : class
        {
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            var expression = specification.Internal.QuerySpecification.AsExpression();
            var projectedQuery = query.ProjectTo<T>(configuration).DecompileAsync();
            return await (expression == null ? projectedQuery.SingleAsync() : projectedQuery.SingleAsync(expression)).ConfigureAwait(false);
        }

        public static async Task<T> SingleOrDefaultAsync<TEntity, T>(this IQueryable<TEntity> query, ISpecification<T> specification, IConfigurationProvider configuration)
            where TEntity : class
            where T : class
        {
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            var expression = specification.Internal.QuerySpecification.AsExpression();
            var projectedQuery = query.ProjectTo<T>(configuration).DecompileAsync();
            return await (expression == null ? projectedQuery.SingleOrDefaultAsync() : projectedQuery.SingleOrDefaultAsync(expression)).ConfigureAwait(false);
        }
    }
}
