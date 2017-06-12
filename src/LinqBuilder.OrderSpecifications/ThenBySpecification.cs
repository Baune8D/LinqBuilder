using System.Collections.Generic;
using System.Linq;

namespace LinqBuilder.Ordering
{
    public class ThenBySpecification<T> : IQueryOrderSpecification<T>
    {
        public List<OrderExpression<T>> OrderList;

        public ThenBySpecification(List<OrderExpression<T>> orderList, IOrderBySpecification<T> right)
        {
            OrderList = orderList;

            OrderList.Add(new OrderExpression<T>
            {
                Expression = right.AsExpression(),
                Order = right.Order
            });
        }

        public ThenBySpecification<T> ThenBy(IOrderBySpecification<T> other)
        {
            return new ThenBySpecification<T>(OrderList, other);
        }

        public IOrderedQueryable<T> Invoke(IQueryable<T> query)
        {
            var orderBy = OrderList[0];

            var orderedQuery = orderBy.Order == Order.Descending
                ? query.OrderByDescending(orderBy.Expression)
                : query.OrderBy(orderBy.Expression);

            for (var i = 1; i < OrderList.Count; i++)
            {
                orderBy = OrderList[i];

                orderedQuery = orderBy.Order == Order.Descending
                    ? orderedQuery.ThenByDescending(orderBy.Expression)
                    : orderedQuery.ThenBy(orderBy.Expression);
            }

            return orderedQuery;
        }

        public IOrderedEnumerable<T> Invoke(IEnumerable<T> collection)
        {
            var orderBy = OrderList[0];

            var orderedQuery = orderBy.Order == Order.Descending
                ? collection.OrderByDescending(orderBy.Expression.Compile())
                : collection.OrderBy(orderBy.Expression.Compile());

            for (var i = 1; i < OrderList.Count; i++)
            {
                orderBy = OrderList[i];

                orderedQuery = orderBy.Order == Order.Descending
                    ? orderedQuery.ThenByDescending(orderBy.Expression.Compile())
                    : orderedQuery.ThenBy(orderBy.Expression.Compile());
            }

            return orderedQuery;
        }
    }
}
