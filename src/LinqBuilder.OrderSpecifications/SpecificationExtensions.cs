using System.Collections.Generic;
using LinqBuilder.Specifications;

namespace LinqBuilder.OrderSpecifications
{
    public static class SpecificationExtensions
    {
        public static ICompositeOrderSpecification<T> OrderBy<T>(this IFilterSpecification<T> specification, OrderSpecification<T> orderSpecification)
            where T : class
        {
            return new CompositeOrderSpecification<T>(specification, new List<IOrderSpecification<T>>(), orderSpecification);
        }
    }
}
