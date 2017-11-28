using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LinqBuilder.OrderSpecifications
{
    public abstract class OrderSpecification<T> : IOrderSpecification<T>
    {
        private readonly Order _order;

        protected OrderSpecification(Order order)
        {
            _order = order;
        }

        public ICompositeSpecification<T> ThenBy(OrderSpecification<T> other)
        {
            var orderList = new List<OrderSpecification<T>> { this };
            return new CompositeOrderSpecification<T>(orderList, other);
        }

        public ICompositeSpecification<T> Skip(int count)
        {
            return new CompositeOrderSpecification<T>(new List<OrderSpecification<T>>(), this, count);
        }

        public ICompositeSpecification<T> Take(int count)
        {
            return new CompositeOrderSpecification<T>(new List<OrderSpecification<T>>(), this, null, count);
        }

        public IOrderedQueryable<T> Invoke(IQueryable<T> query)
        {
            return _order == Order.Descending
                ? query.OrderByDescending(AsExpression())
                : query.OrderBy(AsExpression());
        }

        public IOrderedQueryable<T> Invoke(IOrderedQueryable<T> query)
        {
            return _order == Order.Descending
                ? query.ThenByDescending(AsExpression())
                : query.ThenBy(AsExpression());
        }

        public IOrderedEnumerable<T> Invoke(IEnumerable<T> collection)
        {
            return _order == Order.Descending
                ? collection.OrderByDescending(AsExpression().Compile())
                : collection.OrderBy(AsExpression().Compile());
        }

        public IOrderedEnumerable<T> Invoke(IOrderedEnumerable<T> collection)
        {
            return _order == Order.Descending
                ? collection.ThenByDescending(AsExpression().Compile())
                : collection.ThenBy(AsExpression().Compile());
        }

        public abstract Expression<Func<T, IComparable>> AsExpression();
    }
}
