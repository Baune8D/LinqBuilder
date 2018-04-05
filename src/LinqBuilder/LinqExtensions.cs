using System.Collections.Generic;
using System.Linq;

namespace LinqBuilder
{
    public static class LinqExtensions
    {
        public static IQueryable<TEntity> Where<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification)
        {
            if (specification == null) throw Exceptions.SpecificationCannotBeNull(nameof(specification));
            return query.Where(specification.AsExpression());
        }

        public static IEnumerable<TEntity> Where<TEntity>(this IEnumerable<TEntity> collection, ISpecification<TEntity> specification)
        {
            if (specification == null) throw Exceptions.SpecificationCannotBeNull(nameof(specification));
            return collection.Where(specification.AsFunc());
        }

        public static bool Any<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification)
        {
            if (specification == null) throw Exceptions.SpecificationCannotBeNull(nameof(specification));
            return query.Any(specification.AsExpression());
        }

        public static bool Any<TEntity>(this IEnumerable<TEntity> collection, ISpecification<TEntity> specification)
        {
            if (specification == null) throw Exceptions.SpecificationCannotBeNull(nameof(specification));
            return collection.Any(specification.AsFunc());
        }

        public static bool All<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification)
        {
            if (specification == null) throw Exceptions.SpecificationCannotBeNull(nameof(specification));
            return query.All(specification.AsExpression());
        }

        public static bool All<TEntity>(this IEnumerable<TEntity> collection, ISpecification<TEntity> specification)
        {
            if (specification == null) throw Exceptions.SpecificationCannotBeNull(nameof(specification));
            return collection.All(specification.AsFunc());
        }
    }
}
