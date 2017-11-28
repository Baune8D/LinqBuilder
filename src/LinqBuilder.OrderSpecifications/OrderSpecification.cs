using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LinqBuilder.OrderSpecifications
{
    public abstract class OrderSpecification<T, TKey> : IOrderSpecification<T>
        where T : class
    {
        private readonly Order _order;

        protected OrderSpecification(Order order)
        {
            _order = order;
        }

        public ICompositeOrderSpecification<T> ThenBy(IOrderSpecification<T> other)
        {
            var orderList = new List<IOrderSpecification<T>> { this };
            return new CompositeOrderSpecification<T>(orderList, other);
        }

        public ICompositeOrderSpecification<T> Skip(int count)
        {
            return new CompositeOrderSpecification<T>(new List<IOrderSpecification<T>>(), this, count);
        }

        public ICompositeOrderSpecification<T> Take(int count)
        {
            return new CompositeOrderSpecification<T>(new List<IOrderSpecification<T>>(), this, null, count);
        }

        public IOrderedQueryable<T> InvokeOrdered(IOrderedQueryable<T> query)
        {
            return _order == Order.Descending
                ? query.ThenByDescending(AsExpression())
                : query.ThenBy(AsExpression());
        }

        public IOrderedQueryable<T> InvokeOrdered(IQueryable<T> query)
        {
            return _order == Order.Descending
                ? query.OrderByDescending(AsExpression())
                : query.OrderBy(AsExpression());
        }

        public IQueryable<T> Invoke(IQueryable<T> query)
        {
            return InvokeOrdered(query).AsQueryable();
        }

        public IOrderedEnumerable<T> InvokeOrdered(IOrderedEnumerable<T> collection)
        {
            return _order == Order.Descending
                ? collection.ThenByDescending(AsExpression().Compile())
                : collection.ThenBy(AsExpression().Compile());
        }

        public IOrderedEnumerable<T> InvokeOrdered(IEnumerable<T> collection)
        {
            return _order == Order.Descending
                ? collection.OrderByDescending(AsExpression().Compile())
                : collection.OrderBy(AsExpression().Compile());
        }

        public IEnumerable<T> Invoke(IEnumerable<T> collection)
        {
            return InvokeOrdered(collection).AsEnumerable();
        }

        public abstract Expression<Func<T, TKey>> AsExpression();
    }
}
