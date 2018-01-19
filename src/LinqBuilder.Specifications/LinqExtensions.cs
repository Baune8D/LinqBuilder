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
            return specification.Invoke(query);
        }

        public static IEnumerable<TEntity> Where<TEntity>(this IEnumerable<TEntity> query, IFilterSpecification<TEntity> specification)
        {
            if (specification == null) throw new ArgumentNullException(nameof(specification));
            return specification.Invoke(query);
        }
    }
}
