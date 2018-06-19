using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LinqBuilder.Core;

namespace LinqBuilder.OrderBy
{
    public class OrderSpecification<TEntity, TResult> : IOrderSpecification<TEntity>
        where TEntity : class
    {
        private readonly Expression<Func<TEntity, TResult>> _expression;
        private Func<TEntity, TResult> _func;

        private readonly Sort _sort;

        public OrderSpecification(Sort sort = Sort.Ascending) : this(entity => default, sort) { }

        public OrderSpecification(Expression<Func<TEntity, TResult>> expression, Sort sort = Sort.Ascending)
        {
            _expression = expression;
            _sort = sort;
        }

        public LinqBuilder<TEntity> GetLinqBuilder()
        {
            return new LinqBuilder<TEntity>(new Specification<TEntity>(), this);
        }

        public virtual Expression<Func<TEntity, TResult>> AsExpression()
        {
            return _expression;
        }

        public Func<TEntity, TResult> AsFunc()
        {
            return _func ?? (_func = AsExpression().Compile());
        }

        public static IOrderSpecification<TEntity> New(Sort sort = Sort.Ascending)
        {
            return new OrderSpecification<TEntity, TResult>(sort);
        }

        public static IOrderSpecification<TEntity> New(Expression<Func<TEntity, TResult>> expression, Sort sort = Sort.Ascending)
        {
            return new OrderSpecification<TEntity, TResult>(expression, sort);
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
    }
}
