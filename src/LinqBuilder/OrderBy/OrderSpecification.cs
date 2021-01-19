using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LinqBuilder.Internal;

namespace LinqBuilder.OrderBy
{
    public class OrderSpecification<TEntity, TResult> : IOrderSpecification<TEntity>
        where TEntity : class
    {
        private readonly Sort _sort;
        private readonly Expression<Func<TEntity, TResult>>? _expression;
        private Func<TEntity, TResult>? _func;

        public OrderSpecification(Expression<Func<TEntity, TResult>> expression, Sort sort = Sort.Ascending)
            : this(sort)
        {
            _expression = expression;
        }

        protected OrderSpecification(Sort sort = Sort.Ascending)
        {
            _sort = sort;
        }

        public SpecificationBase<TEntity> Internal => new(this);

        public static IOrderSpecification<TEntity> New(Expression<Func<TEntity, TResult>> expression, Sort sort = Sort.Ascending)
        {
            return new OrderSpecification<TEntity, TResult>(expression, sort);
        }

        public virtual Expression<Func<TEntity, TResult>>? AsExpression()
        {
            return _expression;
        }

        public Func<TEntity, TResult>? AsFunc()
        {
            var expression = AsExpression();
            if (expression == null)
            {
                return null;
            }

            return _func ??= expression.Compile();
        }

        public IOrderedQueryable<TEntity> InvokeSort(IQueryable<TEntity> query)
        {
            var expression = AsExpression();
            if (expression == null)
            {
                throw new InvalidOperationException("AsExpression returns null");
            }

            return _sort == Sort.Descending
                ? query.OrderByDescending(expression)
                : query.OrderBy(expression);
        }

        public IOrderedEnumerable<TEntity> InvokeSort(IEnumerable<TEntity> collection)
        {
            var func = AsFunc();
            if (func == null)
            {
                throw new InvalidOperationException("AsExpression returns null");
            }

            return _sort == Sort.Descending
                ? collection.OrderByDescending(func)
                : collection.OrderBy(func);
        }

        public IOrderedQueryable<TEntity> InvokeSort(IOrderedQueryable<TEntity> query)
        {
            var expression = AsExpression();
            if (expression == null)
            {
                throw new InvalidOperationException("AsExpression returns null");
            }

            return _sort == Sort.Descending
                ? query.ThenByDescending(expression)
                : query.ThenBy(expression);
        }

        public IOrderedEnumerable<TEntity> InvokeSort(IOrderedEnumerable<TEntity> collection)
        {
            var func = AsFunc();
            if (func == null)
            {
                throw new InvalidOperationException("AsExpression returns null");
            }

            return _sort == Sort.Descending
                ? collection.ThenByDescending(func)
                : collection.ThenBy(func);
        }
    }
}
