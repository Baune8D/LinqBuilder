using System;
using System.Collections.Generic;
using System.Linq;
using LinqBuilder.Specifications;

namespace LinqBuilder.OrderSpecifications
{
    public static class LinqExtensions
    {
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> query, IOrderSpecification<T> specification)
        {
            if (specification == null) throw new ArgumentNullException(nameof(specification));
            return specification.InvokeOrdered(query);
        }

        public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> query, IOrderSpecification<T> specification)
        {
            if (specification == null) throw new ArgumentNullException(nameof(specification));
            return specification.InvokeOrdered(query);
        }

        public static IOrderedEnumerable<T> OrderBy<T>(this IEnumerable<T> query, IOrderSpecification<T> specification)
        {
            if (specification == null) throw new ArgumentNullException(nameof(specification));
            return specification.InvokeOrdered(query);
        }

        public static IOrderedEnumerable<T> ThenBy<T>(this IOrderedEnumerable<T> query, IOrderSpecification<T> specification)
        {
            if (specification == null) throw new ArgumentNullException(nameof(specification));
            return specification.InvokeOrdered(query);
        }

        public static IQueryable<T> ExeSpec<T>(this IQueryable<T> query, ISpecification<T> specification)
        {
            if (specification == null) throw new ArgumentNullException(nameof(specification));
            return specification.Invoke(query);
        }

        public static IEnumerable<T> ExeSpec<T>(this IEnumerable<T> query, ISpecification<T> specification)
        {
            if (specification == null) throw new ArgumentNullException(nameof(specification));
            return specification.Invoke(query);
        }
    }
}
