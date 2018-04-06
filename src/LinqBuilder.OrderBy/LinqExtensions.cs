using System.Collections.Generic;
using System.Linq;

namespace LinqBuilder.OrderBy
{
    public static class LinqExtensions
    {
        public static IOrderedQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> query, IOrderSpecification<TEntity> orderSpecification)
        {
            if (orderSpecification == null) throw Exceptions.SpecificationCannotBeNull(nameof(orderSpecification));
            return orderSpecification.InvokeSort(query);
        }

        public static IOrderedEnumerable<TEntity> OrderBy<TEntity>(this IEnumerable<TEntity> collection, IOrderSpecification<TEntity> orderSpecification)
        {
            if (orderSpecification == null) throw Exceptions.SpecificationCannotBeNull(nameof(orderSpecification));
            return orderSpecification.InvokeSort(collection);
        }

        public static IOrderedQueryable<TEntity> ThenBy<TEntity>(this IOrderedQueryable<TEntity> query, IOrderSpecification<TEntity> orderSpecification)
        {
            if (orderSpecification == null) throw Exceptions.SpecificationCannotBeNull(nameof(orderSpecification));
            return orderSpecification.InvokeSort(query);
        }

        public static IOrderedEnumerable<TEntity> ThenBy<TEntity>(this IOrderedEnumerable<TEntity> collection, IOrderSpecification<TEntity> orderSpecification)
        {
            if (orderSpecification == null) throw Exceptions.SpecificationCannotBeNull(nameof(orderSpecification));
            return orderSpecification.InvokeSort(collection);
        }

        public static IQueryable<TEntity> ExeQuery<TEntity>(this IQueryable<TEntity> query, ISpecificationQuery<TEntity> specificationQuery)
        {
            if (specificationQuery == null) throw Exceptions.SpecificationCannotBeNull(nameof(specificationQuery));
            return specificationQuery.Invoke(query);
        }

        public static IEnumerable<TEntity> ExeQuery<TEntity>(this IEnumerable<TEntity> collection, ISpecificationQuery<TEntity> specificationQuery)
        {
            if (specificationQuery == null) throw Exceptions.SpecificationCannotBeNull(nameof(specificationQuery));
            return specificationQuery.Invoke(collection);
        }
    }
}
