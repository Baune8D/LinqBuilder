using System;
using System.Linq.Expressions;

namespace LinqBuilder.OrderSpecifications
{
    public class OrderExpression<T>
    {
        public Expression<Func<T, IComparable>> Expression { get; set; }
        public Order Order { get; set; }
    }
}
