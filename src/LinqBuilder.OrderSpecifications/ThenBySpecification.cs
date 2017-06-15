using System.Collections.Generic;
using System.Linq;

namespace LinqBuilder.OrderSpecifications
{
    public class ThenBySpecification<T> : IOrderSpecification<T>
    {
        public List<IOrderSpecification<T>> OrderList;

        public ThenBySpecification(List<IOrderSpecification<T>> orderList, IOrderSpecification<T> right)
        {
            OrderList = orderList;
            OrderList.Add(right);
        }

        public ThenBySpecification<T> ThenBy(IOrderSpecification<T> other)
        {
            return new ThenBySpecification<T>(OrderList, other);
        }

        public IOrderedQueryable<T> Invoke(IQueryable<T> query)
        {
            var orderedQuery = OrderList[0].Invoke(query);
            for (var i = 1; i < OrderList.Count; i++)
            {
                orderedQuery = OrderList[i].Invoke(orderedQuery);
            }
            return orderedQuery;
        }

        public IOrderedQueryable<T> Invoke(IOrderedQueryable<T> query)
        {
            return OrderList.Aggregate(query, (current, order) => order.Invoke(current));
        }

        public IOrderedEnumerable<T> Invoke(IEnumerable<T> collection)
        {
            var orderedCollection = OrderList[0].Invoke(collection);
            for (var i = 1; i < OrderList.Count; i++)
            {
                orderedCollection = OrderList[i].Invoke(orderedCollection);
            }
            return orderedCollection;
        }

        public IOrderedEnumerable<T> Invoke(IOrderedEnumerable<T> collection)
        {
            return OrderList.Aggregate(collection, (current, order) => order.Invoke(current));
        }
    }
}
