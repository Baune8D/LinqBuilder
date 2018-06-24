﻿using System;
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

        public static ISpecification<TEntity> UseOrdering<TEntity>(this ISpecification<TEntity> specification, ISpecification<TEntity> orderedSpecification)
            where TEntity : class
        {
            var configuration = specification.Internal;
            var orderedConfiguration = orderedSpecification.Internal;
            configuration.OrderSpecifications.AddRange(orderedConfiguration.OrderSpecifications);
            configuration.Skip = orderedConfiguration.Skip;
            configuration.Take = orderedConfiguration.Take;
            return configuration;
        }

        public static bool IsOrdered<TEntity>(this ISpecification<TEntity> specification)
            where TEntity : class
        {
            return specification.Internal.OrderSpecifications.Any();
        }

        public static bool HasSkip<TEntity>(this ISpecification<TEntity> specification)
            where TEntity : class
        {
            return specification.Internal.Skip != null;
        }

        public static bool HasTake<TEntity>(this ISpecification<TEntity> specification)
            where TEntity : class
        {
            return specification.Internal.Take != null;
        }

        public static ISpecification<TEntity> Skip<TEntity>(this ISpecification<TEntity> specification, int count)
            where TEntity : class
        {
            var configuration = specification.Internal;
            configuration.Skip = count;
            return configuration;
        }

        public static ISpecification<TEntity> Take<TEntity>(this ISpecification<TEntity> specification, int count)
            where TEntity : class
        {
            var configuration = specification.Internal;
            configuration.Take = count;
            return configuration;
        }

        public static ISpecification<TEntity> Paginate<TEntity>(this ISpecification<TEntity> specification, int pageNo, int pageSize)
            where TEntity : class
        {
            if (pageNo < 1) throw new ArgumentException("Cannot be less than 1!", nameof(pageNo));
            if (pageSize < 1) throw new ArgumentException("Cannot be less than 1!", nameof(pageSize));
            var configuration = specification.Internal;
            configuration.Skip = (pageNo - 1) * pageSize;
            configuration.Take = pageSize;
            return configuration;
        }

        private static IOrderedSpecification<TEntity> AddOrderSpecification<TEntity>(ISpecification<TEntity> specification, IOrderSpecification<TEntity> orderSpecification)
            where TEntity : class
        {
            var configuration = specification.Internal;
            configuration.OrderSpecifications.Add(orderSpecification);
            return configuration;
        }
    }
}
