using System.Collections.Generic;
using LinqBuilder.Specifications;

namespace LinqBuilder.OrderSpecifications
{
    public static class SpecificationExtensions
    {
        public static ICompositeOrderSpecification<TEntity> OrderBy<TEntity>(this IFilterSpecification<TEntity> specification, IOrderSpecification<TEntity> orderSpecification)
            where TEntity : class
        {
            return new CompositeOrderSpecification<TEntity>(specification, new List<IOrderSpecification<TEntity>>(), orderSpecification);
        }
    }
}
