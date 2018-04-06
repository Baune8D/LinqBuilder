using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LinqBuilder.OrderBy
{
    public class OrderSpecification<TEntity, TKey> : SpecificationQuery<TEntity, TKey>, IOrderSpecification<TEntity>
        where TEntity : class
    {
        private readonly Sort _sort;

        public OrderSpecification(Sort sort = Sort.Ascending) : this(entity => default, sort) { }

        public OrderSpecification(Expression<Func<TEntity, TKey>> expression, Sort sort = Sort.Ascending) : base(expression)
        {
            _sort = sort;
        }

        public IOrderedSpecification<TEntity> ThenBy(IOrderSpecification<TEntity> other)
        {
            var orderList = new List<IOrderSpecification<TEntity>> { this };
            return new OrderedSpecification<TEntity>(orderList, other);
        }

        public IOrderedSpecification<TEntity> Skip(int count)
        {
            return new OrderedSpecification<TEntity>(new List<IOrderSpecification<TEntity>>(), this, count);
        }

        public IOrderedSpecification<TEntity> Take(int count)
        {
            return new OrderedSpecification<TEntity>(new List<IOrderSpecification<TEntity>>(), this, null, count);
        }

        public IOrderedQueryable<TEntity> InvokeSort(IQueryable<TEntity> query)
        {
            return _sort == Sort.Descending
                ? query.OrderByDescending(AsExpression())
                : query.OrderBy(AsExpression());
        }

        public IOrderedEnumerable<TEntity> InvokeSort(IEnumerable<TEntity> collection)
        {
            return _sort == Sort.Descending
                ? collection.OrderByDescending(AsFunc())
                : collection.OrderBy(AsFunc());
        }

        public IOrderedQueryable<TEntity> InvokeSort(IOrderedQueryable<TEntity> query)
        {
            return _sort == Sort.Descending
                ? query.ThenByDescending(AsExpression())
                : query.ThenBy(AsExpression());
        }

        public IOrderedEnumerable<TEntity> InvokeSort(IOrderedEnumerable<TEntity> collection)
        {
            return _sort == Sort.Descending
                ? collection.ThenByDescending(AsFunc())
                : collection.ThenBy(AsFunc());
        }

        public override IQueryable<TEntity> Invoke(IQueryable<TEntity> query)
        {
            return InvokeSort(query);
        }

        public override IEnumerable<TEntity> Invoke(IEnumerable<TEntity> collection)
        {
            return InvokeSort(collection);
        }
    }
}
