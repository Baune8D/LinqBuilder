using System;
using System.Linq.Expressions;

namespace LinqBuilder.Tests.Data.Specifications;

public class MultipleValueSpecification1 : DynamicSpecification<Entity, int>
{
    public MultipleValueSpecification1()
    {
    }

    public MultipleValueSpecification1(int value1)
        : base(value1)
    {
    }

    public override Expression<Func<Entity, bool>> AsExpression()
    {
        return entity => entity.Value1 == Value;
    }
}
