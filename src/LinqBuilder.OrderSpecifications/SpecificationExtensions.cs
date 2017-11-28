using System.Collections.Generic;
using LinqBuilder.Specifications;

namespace LinqBuilder.OrderSpecifications
{
    public static class SpecificationExtensions
    {
        public static ICompositeOrderSpecification<T> OrderBy<T, TKey>(this IFilterSpecification<T> specification, OrderSpecification<T, TKey> orderSpecification)
            where T : class
        {
            return new CompositeOrderSpecification<T>(specification, new List<IOrderSpecification<T>>(), orderSpecification);
        }
    }
}
