using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using LinqBuilder.Internal;

namespace LinqBuilder
{
    public static class LinqExtensions
    {
        public static IQueryable<TEntity> Where<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification)
            where TEntity : class
        {
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            var expression = specification.Internal.QuerySpecification.AsExpression();
            return expression == null ? query : query.Where(expression);
        }

        public static IEnumerable<TEntity> Where<TEntity>(this IEnumerable<TEntity> collection, ISpecification<TEntity> specification)
            where TEntity : class
        {
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            var func = specification.Internal.QuerySpecification.AsFunc();
            return func == null ? collection : collection.Where(func);
        }

        public static bool Any<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification)
            where TEntity : class
        {
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            var expression = specification.Internal.QuerySpecification.AsExpression();
            return expression == null ? query.Any() : query.Any(expression);
        }

        public static bool Any<TEntity>(this IEnumerable<TEntity> collection, ISpecification<TEntity> specification)
            where TEntity : class
        {
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            var func = specification.Internal.QuerySpecification.AsFunc();
            return func == null ? collection.Any() : collection.Any(func);
        }

        public static bool All<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification)
            where TEntity : class
        {
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            var expression = specification.Internal.QuerySpecification.AsExpression();
            return expression == null || query.All(expression);
        }

        public static bool All<TEntity>(this IEnumerable<TEntity> collection, ISpecification<TEntity> specification)
            where TEntity : class
        {
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            var func = specification.Internal.QuerySpecification.AsFunc();
            return func == null || collection.All(func);
        }

        public static int Count<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification)
            where TEntity : class
        {
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            var expression = specification.Internal.QuerySpecification.AsExpression();
            return expression == null ? query.Count() : query.Count(expression);
        }

        public static int Count<TEntity>(this IEnumerable<TEntity> collection, ISpecification<TEntity> specification)
            where TEntity : class
        {
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            var func = specification.Internal.QuerySpecification.AsFunc();
            return func == null ? collection.Count() : collection.Count(func);
        }

        public static TEntity First<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification)
            where TEntity : class
        {
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            var expression = specification.Internal.QuerySpecification.AsExpression();
            return expression == null ? query.First() : query.First(expression);
        }

        public static TEntity First<TEntity>(this IEnumerable<TEntity> collection, ISpecification<TEntity> specification)
            where TEntity : class
        {
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            var func = specification.Internal.QuerySpecification.AsFunc();
            return func == null ? collection.First() : collection.First(func);
        }

        public static TEntity? FirstOrDefault<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification)
            where TEntity : class
        {
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            var expression = specification.Internal.QuerySpecification.AsExpression();
            return expression == null ? query.FirstOrDefault() : query.FirstOrDefault(expression);
        }

        public static TEntity? FirstOrDefault<TEntity>(this IEnumerable<TEntity> collection, ISpecification<TEntity> specification)
            where TEntity : class
        {
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            var func = specification.Internal.QuerySpecification.AsFunc();
            return func == null ? collection.FirstOrDefault() : collection.FirstOrDefault(func);
        }

        [SuppressMessage("Microsoft.Naming", "CA1720", Justification = "Wrapper around existing Single function.")]
        public static TEntity Single<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification)
            where TEntity : class
        {
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            var expression = specification.Internal.QuerySpecification.AsExpression();
            return expression == null ? query.Single() : query.Single(expression);
        }

        [SuppressMessage("Microsoft.Naming", "CA1720", Justification = "Wrapper around existing Single function.")]
        public static TEntity Single<TEntity>(this IEnumerable<TEntity> collection, ISpecification<TEntity> specification)
            where TEntity : class
        {
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            var func = specification.Internal.QuerySpecification.AsFunc();
            return func == null ? collection.Single() : collection.Single(func);
        }

        public static TEntity? SingleOrDefault<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification)
            where TEntity : class
        {
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            var expression = specification.Internal.QuerySpecification.AsExpression();
            return expression == null ? query.SingleOrDefault() : query.SingleOrDefault(expression);
        }

        public static TEntity? SingleOrDefault<TEntity>(this IEnumerable<TEntity> collection, ISpecification<TEntity> specification)
            where TEntity : class
        {
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            var func = specification.Internal.QuerySpecification.AsFunc();
            return func == null ? collection.SingleOrDefault() : collection.SingleOrDefault(func);
        }

        public static IQueryable<TEntity> ExeSpec<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification, bool skipSort = false)
            where TEntity : class
        {
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            var configuration = specification.Internal;
            var querySpecification = configuration.QuerySpecification;
            var orderSpecifications = configuration.OrderSpecifications;
            var expression = querySpecification.AsExpression();

            if (expression != null)
            {
                query = query.Where(expression);
            }

            if (skipSort)
            {
                return query;
            }

            IOrderedQueryable<TEntity>? ordered = null;
            foreach (var orderSpecification in orderSpecifications)
            {
                ordered = ordered == null
                    ? orderSpecification.InvokeSort(query)
                    : orderSpecification.InvokeSort(ordered);
            }

            return SkipTake(ordered ?? query, configuration);
        }

        public static IEnumerable<TEntity> ExeSpec<TEntity>(this IEnumerable<TEntity> collection, ISpecification<TEntity> specification, bool skipSort = false)
            where TEntity : class
        {
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            var configuration = specification.Internal;
            var querySpecification = configuration.QuerySpecification;
            var orderSpecifications = configuration.OrderSpecifications;
            var func = querySpecification.AsFunc();

            if (func != null)
            {
                collection = collection.Where(func);
            }

            if (skipSort)
            {
                return collection;
            }

            var list = collection.ToList();

            IOrderedEnumerable<TEntity>? ordered = null;
            foreach (var orderSpecification in orderSpecifications)
            {
                ordered = ordered == null
                    ? orderSpecification.InvokeSort(list)
                    : orderSpecification.InvokeSort(ordered);
            }

            return SkipTake(ordered ?? collection, configuration);
        }

        private static IQueryable<TEntity> SkipTake<TEntity>(IQueryable<TEntity> query, SpecificationBase<TEntity> configuration)
            where TEntity : class
        {
            if (configuration.Skip.HasValue)
            {
                query = query.Skip(configuration.Skip.Value);
            }

            if (configuration.Take.HasValue)
            {
                query = query.Take(configuration.Take.Value);
            }

            return query;
        }

        private static IEnumerable<TEntity> SkipTake<TEntity>(IEnumerable<TEntity> collection, SpecificationBase<TEntity> configuration)
            where TEntity : class
        {
            if (configuration.Skip.HasValue)
            {
                collection = collection.Skip(configuration.Skip.Value);
            }

            if (configuration.Take.HasValue)
            {
                collection = collection.Take(configuration.Take.Value);
            }

            return collection;
        }
    }
}
