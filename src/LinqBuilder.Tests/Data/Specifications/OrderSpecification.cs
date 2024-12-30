using System;
using System.Linq.Expressions;
using LinqBuilder.OrderBy;

namespace LinqBuilder.Tests.Data.Specifications;

public class OrderSpecification : OrderSpecification<Entity, int>
{
    public OrderSpecification(Sort sort = Sort.Ascending)
        : base(sort)
    {
    }

    public override Expression<Func<Entity, int>> AsExpression()
    {
        return entity => entity.Value1;
    }
}
