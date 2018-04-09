﻿using System;
using System.Linq.Expressions;

namespace LinqBuilder.OrderBy
{
    // Just an alias of OrderSpecification
    public class OrderSpec<TEntity, TResult> : OrderSpecification<TEntity, TResult>
        where TEntity : class
    {
        public OrderSpec(Sort sort = Sort.Ascending) : base(sort) { }

        public OrderSpec(Expression<Func<TEntity, TResult>> expression, Sort sort = Sort.Ascending) : base(expression, sort) { }
    }
}