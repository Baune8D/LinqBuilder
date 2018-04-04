using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqBuilder.Specifications
{
    public static class LinqExtensions
    {
        public static IQueryable<TEntity> Where<TEntity>(this IQueryable<TEntity> query, IFilterSpecification<TEntity> specification)
        {
            if (specification == null) throw new ArgumentNullException(nameof(specification));
            return query.Where(specification.AsExpression());
        }

        public static IEnumerable<TEntity> Where<TEntity>(this IEnumerable<TEntity> collection, IFilterSpecification<TEntity> specification)
        {
            if (specification == null) throw new ArgumentNullException(nameof(specification));
            return collection.Where(specification.AsExpression().Compile());
        }

        public static bool Any<TEntity>(this IQueryable<TEntity> query, IFilterSpecification<TEntity> specification)
        {
            if (specification == null) throw new ArgumentNullException(nameof(specification));
            return query.Any(specification.AsExpression());
        }

        public static bool Any<TEntity>(this IEnumerable<TEntity> collection, IFilterSpecification<TEntity> specification)
        {
            if (specification == null) throw new ArgumentNullException(nameof(specification));
            return collection.Any(specification.AsExpression().Compile());
        }

        public static bool All<TEntity>(this IQueryable<TEntity> query, IFilterSpecification<TEntity> specification)
        {
            if (specification == null) throw new ArgumentNullException(nameof(specification));
            return query.All(specification.AsExpression());
        }

        public static bool All<TEntity>(this IEnumerable<TEntity> collection, IFilterSpecification<TEntity> specification)
        {
            if (specification == null) throw new ArgumentNullException(nameof(specification));
            return collection.All(specification.AsExpression().Compile());
        }
    }
}
