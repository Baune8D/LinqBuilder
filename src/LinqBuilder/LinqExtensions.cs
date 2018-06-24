using System.Collections.Generic;
using System.Linq;
using LinqBuilder.Core;

namespace LinqBuilder
{
    public static class LinqExtensions
    {
        public static IQueryable<TEntity> Where<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification)
            where TEntity : class
        {
            return query.Where(specification.Internal.QuerySpecification.AsExpression());
        }

        public static IEnumerable<TEntity> Where<TEntity>(this IEnumerable<TEntity> collection, ISpecification<TEntity> specification)
            where TEntity : class
        {
            return collection.Where(specification.Internal.QuerySpecification.AsFunc());
        }

        public static bool Any<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification)
            where TEntity : class
        {
            return query.Any(specification.Internal.QuerySpecification.AsExpression());
        }

        public static bool Any<TEntity>(this IEnumerable<TEntity> collection, ISpecification<TEntity> specification)
            where TEntity : class
        {
            return collection.Any(specification.Internal.QuerySpecification.AsFunc());
        }

        public static bool All<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification)
            where TEntity : class
        {
            return query.All(specification.Internal.QuerySpecification.AsExpression());
        }

        public static bool All<TEntity>(this IEnumerable<TEntity> collection, ISpecification<TEntity> specification)
            where TEntity : class
        {
            return collection.All(specification.Internal.QuerySpecification.AsFunc());
        }

        public static int Count<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification)
            where TEntity : class
        {
            return query.Count(specification.Internal.QuerySpecification.AsExpression());
        }

        public static int Count<TEntity>(this IEnumerable<TEntity> collection, ISpecification<TEntity> specification)
            where TEntity : class
        {
            return collection.Count(specification.Internal.QuerySpecification.AsFunc());
        }

        public static TEntity First<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification)
            where TEntity : class
        {
            return query.First(specification.Internal.QuerySpecification.AsExpression());
        }

        public static TEntity First<TEntity>(this IEnumerable<TEntity> collection, ISpecification<TEntity> specification)
            where TEntity : class
        {
            return collection.First(specification.Internal.QuerySpecification.AsFunc());
        }

        public static TEntity FirstOrDefault<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification)
            where TEntity : class
        {
            return query.FirstOrDefault(specification.Internal.QuerySpecification.AsExpression());
        }

        public static TEntity FirstOrDefault<TEntity>(this IEnumerable<TEntity> collection, ISpecification<TEntity> specification)
            where TEntity : class
        {
            return collection.FirstOrDefault(specification.Internal.QuerySpecification.AsFunc());
        }

        public static TEntity Single<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification)
            where TEntity : class
        {
            return query.Single(specification.Internal.QuerySpecification.AsExpression());
        }

        public static TEntity Single<TEntity>(this IEnumerable<TEntity> collection, ISpecification<TEntity> specification)
            where TEntity : class
        {
            return collection.Single(specification.Internal.QuerySpecification.AsFunc());
        }

        public static TEntity SingleOrDefault<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification)
            where TEntity : class
        {
            return query.SingleOrDefault(specification.Internal.QuerySpecification.AsExpression());
        }

        public static TEntity SingleOrDefault<TEntity>(this IEnumerable<TEntity> collection, ISpecification<TEntity> specification)
            where TEntity : class
        {
            return collection.SingleOrDefault(specification.Internal.QuerySpecification.AsFunc());
        }
    }
}
