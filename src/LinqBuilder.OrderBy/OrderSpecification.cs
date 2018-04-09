using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LinqBuilder.OrderBy
{
    public class OrderSpecification<TEntity, TResult> : SpecificationQuery<TEntity, TResult>, IOrderSpecification<TEntity>
        where TEntity : class
    {
        private readonly Sort _sort;

        public OrderSpecification(Sort sort = Sort.Ascending) : this(entity => default, sort) { }

        public OrderSpecification(Expression<Func<TEntity, TResult>> expression, Sort sort = Sort.Ascending) : base(expression)
        {
            _sort = sort;
        }

        public static IOrderSpecification<TEntity> New(Sort sort = Sort.Ascending)
        {
            return new OrderSpecification<TEntity, TResult>(sort);
        }

        public static IOrderSpecification<TEntity> New(Expression<Func<TEntity, TResult>> expression, Sort sort = Sort.Ascending)
        {
            return new OrderSpecification<TEntity, TResult>(expression, sort);
        }

        public IOrderedSpecification<TEntity> ThenBy(IOrderSpecification<TEntity> other)
        {
            return new OrderedSpecification<TEntity>(new Ordering<TEntity>
            {
                OrderList = GetOrderList(this, other),
            });
        }

        public IOrderedSpecification<TEntity> Skip(int count)
        {
            return new OrderedSpecification<TEntity>(new Ordering<TEntity>
            {
                OrderList = GetOrderList(this),
                Skip = count
            });
        }

        public IOrderedSpecification<TEntity> Take(int count)
        {
            return new OrderedSpecification<TEntity>(new Ordering<TEntity>
            {
                OrderList = GetOrderList(this),
                Take = count
            });
        }

        public IOrderedSpecification<TEntity> Paginate(int pageNo, int pageSize)
        {
            return new OrderedSpecification<TEntity>(new Ordering<TEntity>
            {
                OrderList = GetOrderList(this),
                Skip = (pageNo - 1) * pageSize,
                Take = pageSize
            });
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

        private static List<IOrderSpecification<TEntity>> GetOrderList(params IOrderSpecification<TEntity>[] orderSpecifications)
        {
            return orderSpecifications.ToList();
        }
    }
}
