using System.Collections.Generic;
using System.Linq;

namespace LinqBuilder.OrderSpecifications
{
    internal static class OrderHelper
    {
        public static IQueryable<T> SkipTake<T>(IQueryable<T> query, int? skip, int? take)
        {
            if (skip.HasValue) query = query.Skip(skip.Value);
            if (take.HasValue) query = query.Take(take.Value);
            return query;
        }

        public static IEnumerable<T> SkipTake<T>(IEnumerable<T> collection, int? skip, int? take)
        {
            if (skip.HasValue) collection = collection.Skip(skip.Value);
            if (take.HasValue) collection = collection.Take(take.Value);
            return collection;
        }
    }
}
