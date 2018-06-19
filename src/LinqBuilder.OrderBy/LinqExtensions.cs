using System.Collections.Generic;
using System.Linq;
using LinqBuilder.Core;

namespace LinqBuilder.OrderBy
{
    public static class LinqExtensions
    {
        public static IOrderedQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> query, IOrderSpecification<TEntity> orderSpecification)
            where TEntity : class
        {
            return orderSpecification.InvokeSort(query);
        }

        public static IOrderedEnumerable<TEntity> OrderBy<TEntity>(this IEnumerable<TEntity> collection, IOrderSpecification<TEntity> orderSpecification)
            where TEntity : class
        {
            return orderSpecification.InvokeSort(collection);
        }

        public static IOrderedQueryable<TEntity> ThenBy<TEntity>(this IOrderedQueryable<TEntity> query, IOrderSpecification<TEntity> orderSpecification)
            where TEntity : class
        {
            return orderSpecification.InvokeSort(query);
        }

        public static IOrderedEnumerable<TEntity> ThenBy<TEntity>(this IOrderedEnumerable<TEntity> collection, IOrderSpecification<TEntity> orderSpecification)
            where TEntity : class
        {
            return orderSpecification.InvokeSort(collection);
        }

        public static IQueryable<TEntity> ExeQuery<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification, bool skipSort = false)
            where TEntity : class
        {
            var linqBuilder = specification.GetLinqBuilder();
            var querySpecification = linqBuilder.QuerySpecification;
            var orderSpecifications = linqBuilder.OrderSpecifications;

            if (querySpecification != null)
            {
                query = query.Where(querySpecification.AsExpression());
            }
            if (skipSort) return query;

            var ordered = orderSpecifications.FirstOrDefault()?.InvokeSort(query);
            for (var i = 1; i < orderSpecifications.Count; i++)
            {
                ordered = orderSpecifications[i].InvokeSort(ordered);
            }

            return SkipTake(ordered ?? query, linqBuilder);
        }

        public static IEnumerable<TEntity> ExeQuery<TEntity>(this IEnumerable<TEntity> collection, ISpecification<TEntity> specification, bool skipSort = false)
            where TEntity : class
        {
            var linqBuilder = specification.GetLinqBuilder();
            var querySpecification = linqBuilder.QuerySpecification;
            var orderSpecifications = linqBuilder.OrderSpecifications;

            if (querySpecification != null)
            {
                collection = collection.Where(querySpecification.AsFunc());
            }
            if (skipSort) return collection;

            var ordered = orderSpecifications.FirstOrDefault()?.InvokeSort(collection);
            for (var i = 1; i < orderSpecifications.Count; i++)
            {
                ordered = orderSpecifications[i].InvokeSort(ordered);
            }

            return SkipTake(ordered ?? collection, linqBuilder);
        }

        private static IQueryable<TEntity> SkipTake<TEntity>(IQueryable<TEntity> query, LinqBuilder<TEntity> linqBuilder)
            where TEntity : class
        {
            if (linqBuilder.Skip.HasValue) query = query.Skip(linqBuilder.Skip.Value);
            if (linqBuilder.Take.HasValue) query = query.Take(linqBuilder.Take.Value);
            return query;
        }

        private static IEnumerable<TEntity> SkipTake<TEntity>(IEnumerable<TEntity> collection, LinqBuilder<TEntity> linqBuilder)
            where TEntity : class
        {
            if (linqBuilder.Skip.HasValue) collection = collection.Skip(linqBuilder.Skip.Value);
            if (linqBuilder.Take.HasValue) collection = collection.Take(linqBuilder.Take.Value);
            return collection;
        }
    }
}
