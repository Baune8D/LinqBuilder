using System;
using System.Linq;

namespace LinqBuilder.OrderBy
{
    public static class SpecificationExtensions
    {
        public static IOrderedSpecification<TEntity> OrderBy<TEntity>(this ISpecification<TEntity> specification, IOrderSpecification<TEntity> orderSpecification)
            where TEntity : class
        {
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            return AddOrderSpecification(specification, orderSpecification);
        }

        public static IOrderedSpecification<TEntity> ThenBy<TEntity>(this IOrderSpecification<TEntity> specification, IOrderSpecification<TEntity> orderSpecification)
            where TEntity : class
        {
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            return AddOrderSpecification(specification, orderSpecification);
        }

        public static IOrderedSpecification<TEntity> ThenBy<TEntity>(this IOrderedSpecification<TEntity> specification, IOrderSpecification<TEntity> orderSpecification)
            where TEntity : class
        {
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            return AddOrderSpecification(specification, orderSpecification);
        }

        public static ISpecification<TEntity> UseOrdering<TEntity>(this ISpecification<TEntity> specification, ISpecification<TEntity> orderedSpecification)
            where TEntity : class
        {
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            if (orderedSpecification == null)
            {
                throw new ArgumentNullException(nameof(orderedSpecification));
            }

            var configuration = specification.Internal;
            var orderedConfiguration = orderedSpecification.Internal;
            configuration.AddOrderSpecifications(orderedConfiguration.OrderSpecifications.ToArray());
            configuration.Skip = orderedConfiguration.Skip;
            configuration.Take = orderedConfiguration.Take;
            return configuration;
        }

        public static ISpecification<TEntity> Skip<TEntity>(this ISpecification<TEntity> specification, int count)
            where TEntity : class
        {
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            var configuration = specification.Internal.Clone().Internal;
            configuration.Skip = count;
            return configuration;
        }

        public static ISpecification<TEntity> Take<TEntity>(this ISpecification<TEntity> specification, int count)
            where TEntity : class
        {
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            var configuration = specification.Internal.Clone().Internal;
            configuration.Take = count;
            return configuration;
        }

        public static ISpecification<TEntity> Paginate<TEntity>(this ISpecification<TEntity> specification, int pageNo, int pageSize)
            where TEntity : class
        {
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            if (pageNo < 1)
            {
                throw new ArgumentException("Cannot be less than 1!", nameof(pageNo));
            }

            if (pageSize < 1)
            {
                throw new ArgumentException("Cannot be less than 1!", nameof(pageSize));
            }

            var configuration = specification.Internal.Clone().Internal;
            configuration.Skip = (pageNo - 1) * pageSize;
            configuration.Take = pageSize;
            return configuration;
        }

        public static bool IsOrdered<TEntity>(this ISpecification<TEntity> specification)
            where TEntity : class
        {
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            return specification.Internal.OrderSpecifications.Any();
        }

        public static bool HasSkip<TEntity>(this ISpecification<TEntity> specification)
            where TEntity : class
        {
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            return specification.Internal.Skip != null;
        }

        public static bool HasTake<TEntity>(this ISpecification<TEntity> specification)
            where TEntity : class
        {
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            return specification.Internal.Take != null;
        }

        private static IOrderedSpecification<TEntity> AddOrderSpecification<TEntity>(ISpecification<TEntity> specification, IOrderSpecification<TEntity> orderSpecification)
            where TEntity : class
        {
            var configuration = specification.Internal.Clone().Internal;
            configuration.AddOrderSpecifications(orderSpecification);
            return configuration;
        }
    }
}
