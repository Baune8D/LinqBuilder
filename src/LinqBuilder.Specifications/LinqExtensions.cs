using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqBuilder.Specifications
{
    public static class LinqExtensions
    {
        public static IQueryable<T> Where<T>(this IQueryable<T> query, IFilterSpecification<T> specification)
        {
            if (specification == null) throw new ArgumentNullException(nameof(specification));
            return specification.Invoke(query);
        }

        public static IEnumerable<T> Where<T>(this IEnumerable<T> query, IFilterSpecification<T> specification)
        {
            if (specification == null) throw new ArgumentNullException(nameof(specification));
            return specification.Invoke(query);
        }
    }
}
