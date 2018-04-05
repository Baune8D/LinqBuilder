using System;
using System.Linq.Expressions;

namespace LinqBuilder.OrderBy
{
    // Just an alias of OrderSpecification
    public class OrderSpec<TEntity, TKey> : OrderSpecification<TEntity, TKey>
        where TEntity : class
    {
        public OrderSpec(Sort sort = Sort.Ascending) : base(sort) { }

        public OrderSpec(Expression<Func<TEntity, TKey>> expression, Sort sort = Sort.Ascending) : base(expression, sort) { }
    }
}
