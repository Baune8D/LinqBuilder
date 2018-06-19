using System;
using System.Linq;
using LinqBuilder.Core;

namespace LinqBuilder.OrderBy
{
    public static class SpecificationExtensions
    {
        public static IOrderedSpecification<TEntity> OrderBy<TEntity>(this ISpecification<TEntity> specification, IOrderSpecification<TEntity> orderSpecification)
            where TEntity : class
        {
            return AddOrderSpecification(specification, orderSpecification);
        }

        public static IOrderedSpecification<TEntity> ThenBy<TEntity>(this IOrderSpecification<TEntity> specification, IOrderSpecification<TEntity> orderSpecification)
            where TEntity : class
        {
            return AddOrderSpecification(specification, orderSpecification);
        }

        public static IOrderedSpecification<TEntity> ThenBy<TEntity>(this IOrderedSpecification<TEntity> specification, IOrderSpecification<TEntity> orderSpecification)
            where TEntity : class
        {
            return AddOrderSpecification(specification, orderSpecification);
        }

        public static ISpecification<TEntity> UseOrdering<TEntity>(this ISpecification<TEntity> specification, IOrderedSpecification<TEntity> orderedSpecification)
            where TEntity : class
        {
            var linqBuilder = specification.GetLinqBuilder();
            var orderedLinqBuilder = orderedSpecification.GetLinqBuilder();
            linqBuilder.OrderSpecifications.AddRange(orderedLinqBuilder.OrderSpecifications);
            linqBuilder.Skip = orderedLinqBuilder.Skip;
            linqBuilder.Take = orderedLinqBuilder.Take;
            return linqBuilder;
        }

        public static bool IsOrdered<TEntity>(this ISpecification<TEntity> specification)
            where TEntity : class
        {
            return specification.GetLinqBuilder().OrderSpecifications.Any();
        }

        public static IOrderedSpecification<TEntity> Skip<TEntity>(this ISpecification<TEntity> specification, int count)
            where TEntity : class
        {
            var linqBuilder = specification.GetLinqBuilder();
            linqBuilder.Skip = count;
            return linqBuilder;
        }

        public static IOrderedSpecification<TEntity> Take<TEntity>(this ISpecification<TEntity> specification, int count)
            where TEntity : class
        {
            var linqBuilder = specification.GetLinqBuilder();
            linqBuilder.Take = count;
            return linqBuilder;
        }

        public static IOrderedSpecification<TEntity> Paginate<TEntity>(this ISpecification<TEntity> specification, int pageNo, int pageSize)
            where TEntity : class
        {
            if (pageNo < 1) throw new ArgumentException("Cannot be less than 1!", nameof(pageNo));
            if (pageSize < 1) throw new ArgumentException("Cannot be less than 1!", nameof(pageSize));
            var linqBuilder = specification.GetLinqBuilder();
            linqBuilder.Skip = (pageNo - 1) * pageSize;
            linqBuilder.Take = pageSize;
            return linqBuilder;
        }

        private static IOrderedSpecification<TEntity> AddOrderSpecification<TEntity>(ISpecification<TEntity> specification, IOrderSpecification<TEntity> orderSpecification)
            where TEntity : class
        {
            var linqBuilder = specification.GetLinqBuilder();
            linqBuilder.OrderSpecifications.Add(orderSpecification);
            return linqBuilder;
        }
    }
}
