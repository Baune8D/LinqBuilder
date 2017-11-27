using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqBuilder.OrderSpecifications
{
    public static class LinqExtensions
    {
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> query, OrderSpecification<T> specification)
        {
            if (specification == null) throw new ArgumentNullException(nameof(specification));
            return specification.Invoke(query);
        }

        public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> query, OrderSpecification<T> specification)
        {
            if (specification == null) throw new ArgumentNullException(nameof(specification));
            return specification.Invoke(query);
        }

        public static IOrderedEnumerable<T> OrderBy<T>(this IEnumerable<T> query, OrderSpecification<T> specification)
        {
            if (specification == null) throw new ArgumentNullException(nameof(specification));
            return specification.Invoke(query);
        }

        public static IOrderedEnumerable<T> ThenBy<T>(this IOrderedEnumerable<T> query, OrderSpecification<T> specification)
        {
            if (specification == null) throw new ArgumentNullException(nameof(specification));
            return specification.Invoke(query);
        }
    }
}
