using System.Collections.Generic;

namespace LinqBuilder.OrderBy
{
    public static class SpecificationExtensions
    {
        public static IOrderedSpecification<TEntity> OrderBy<TEntity>(this ISpecification<TEntity> specification, IOrderSpecification<TEntity> orderSpecification)
            where TEntity : class
        {
            if (orderSpecification == null) throw Exceptions.SpecificationCannotBeNull(nameof(orderSpecification));
            return new OrderedSpecification<TEntity>(specification, new Ordering<TEntity>
            {
                OrderList = new List<IOrderSpecification<TEntity>> { orderSpecification }
            });
        }

        public static IOrderedSpecification<TEntity> UseOrdering<TEntity>(this ISpecification<TEntity> specification, IOrderedSpecification<TEntity> orderedSpecification)
            where TEntity : class
        {
            if (orderedSpecification == null) throw Exceptions.SpecificationCannotBeNull(nameof(orderedSpecification));
            return new OrderedSpecification<TEntity>(specification, orderedSpecification.GetOrdering());
        }
    }
}
