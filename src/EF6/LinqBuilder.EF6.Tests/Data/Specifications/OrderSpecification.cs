using System;
using System.Linq.Expressions;
using LinqBuilder.OrderBy;

namespace LinqBuilder.EF6.Tests.Data.Specifications;

public class OrderSpecification(Sort sort = Sort.Ascending) : OrderSpecification<SomeEntity, int>(sort)
{
    public override Expression<Func<SomeEntity, int>> AsExpression()
    {
        return entity => entity.Value1;
    }
}
