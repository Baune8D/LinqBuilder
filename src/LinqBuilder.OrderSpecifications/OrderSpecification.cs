using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LinqBuilder.OrderSpecifications
{
    public abstract class OrderSpecification<T> : IOrderSpecification<T>
    {
        public Order Order { get; set; }

        protected OrderSpecification(Order direction)
        {
            Order = direction;
        }

        public ThenBySpecification<T> ThenBy(IOrderSpecification<T> other)
        {
            var orderList = new List<IOrderSpecification<T>> { this };
            return new ThenBySpecification<T>(orderList, other);
        }

        public IOrderedQueryable<T> Invoke(IQueryable<T> query)
        {
            return Order == Order.Descending
                ? query.OrderByDescending(AsExpression())
                : query.OrderBy(AsExpression());
        }

        public IOrderedQueryable<T> Invoke(IOrderedQueryable<T> query)
        {
            return Order == Order.Descending
                ? query.ThenByDescending(AsExpression())
                : query.ThenBy(AsExpression());
        }

        public IOrderedEnumerable<T> Invoke(IEnumerable<T> collection)
        {
            return Order == Order.Descending
                ? collection.OrderByDescending(AsExpression().Compile())
                : collection.OrderBy(AsExpression().Compile());
        }

        public IOrderedEnumerable<T> Invoke(IOrderedEnumerable<T> collection)
        {
            return Order == Order.Descending
                ? collection.ThenByDescending(AsExpression().Compile())
                : collection.ThenBy(AsExpression().Compile());
        }

        public abstract Expression<Func<T, IComparable>> AsExpression();
    }
}
