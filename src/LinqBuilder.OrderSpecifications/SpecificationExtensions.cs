﻿using System.Collections.Generic;
using LinqBuilder.Specifications;

namespace LinqBuilder.OrderSpecifications
{
    public static class SpecificationExtensions
    {
        public static ICompositeOrderSpecification<T> OrderBy<T>(this IFilterSpecification<T> specification, OrderSpecification<T> orderSpecification)
        {
            return new CompositeOrderSpecification<T>(specification, new List<OrderSpecification<T>>(), orderSpecification);
        }
    }
}
