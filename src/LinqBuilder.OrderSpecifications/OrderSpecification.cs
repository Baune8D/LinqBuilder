using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LinqBuilder.OrderSpecifications
{
    public abstract class OrderSpecification<TEntity, TKey> : IOrderSpecification<TEntity>
        where TEntity : class
    {
        private readonly Order _order;

        protected OrderSpecification(Order order)
        {
            _order = order;
        }

        public ICompositeOrderSpecification<TEntity> ThenBy(IOrderSpecification<TEntity> other)
        {
            var orderList = new List<IOrderSpecification<TEntity>> { this };
            return new CompositeOrderSpecification<TEntity>(orderList, other);
        }

        public ICompositeOrderSpecification<TEntity> Skip(int count)
        {
            return new CompositeOrderSpecification<TEntity>(new List<IOrderSpecification<TEntity>>(), this, count);
        }

        public ICompositeOrderSpecification<TEntity> Take(int count)
        {
            return new CompositeOrderSpecification<TEntity>(new List<IOrderSpecification<TEntity>>(), this, null, count);
        }

        public IOrderedQueryable<TEntity> InvokeOrdered(IOrderedQueryable<TEntity> query)
        {
            return _order == Order.Descending
                ? query.ThenByDescending(AsExpression())
                : query.ThenBy(AsExpression());
        }

        public IOrderedQueryable<TEntity> InvokeOrdered(IQueryable<TEntity> query)
        {
            return _order == Order.Descending
                ? query.OrderByDescending(AsExpression())
                : query.OrderBy(AsExpression());
        }

        public IQueryable<TEntity> Invoke(IQueryable<TEntity> query)
        {
            return InvokeOrdered(query).AsQueryable();
        }

        public IOrderedEnumerable<TEntity> InvokeOrdered(IOrderedEnumerable<TEntity> collection)
        {
            return _order == Order.Descending
                ? collection.ThenByDescending(AsExpression().Compile())
                : collection.ThenBy(AsExpression().Compile());
        }

        public IOrderedEnumerable<TEntity> InvokeOrdered(IEnumerable<TEntity> collection)
        {
            return _order == Order.Descending
                ? collection.OrderByDescending(AsExpression().Compile())
                : collection.OrderBy(AsExpression().Compile());
        }

        public IEnumerable<TEntity> Invoke(IEnumerable<TEntity> collection)
        {
            return InvokeOrdered(collection).AsEnumerable();
        }

        public abstract Expression<Func<TEntity, TKey>> AsExpression();
    }
}
