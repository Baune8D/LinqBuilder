using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LinqBuilder.Ordering
{
    public abstract class OrderBySpecification<T> : IOrderBySpecification<T>, IQueryOrderSpecification<T>
    {
        public Order Order { get; set; }

        protected OrderBySpecification(Order direction)
        {
            Order = direction;
        }

        public abstract Expression<Func<T, IComparable>> AsExpression();

        public virtual ThenBySpecification<T> ThenBy(IOrderBySpecification<T> other)
        {
            var orderList = new List<OrderExpression<T>>
            {
                new OrderExpression<T>
                {
                    Expression = AsExpression(),
                    Order = Order
                }
            };

            return new ThenBySpecification<T>(orderList, other);
        }

        public IOrderedQueryable<T> Invoke(IQueryable<T> query)
        {
            return Order == Order.Descending
                ? query.OrderByDescending(AsExpression())
                : query.OrderBy(AsExpression());
        }

        public IOrderedEnumerable<T> Invoke(IEnumerable<T> collection)
        {
            return Order == Order.Descending
                ? collection.OrderByDescending(AsExpression().Compile())
                : collection.OrderBy(AsExpression().Compile());
        }
    }
}
