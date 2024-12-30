using System;
using System.Linq;
using System.Linq.Expressions;

namespace LinqBuilder.EF6.Tests.Data.Specifications;

public class ChildValueSpecification(int value) : Specification<SomeEntity>
{
    public override Expression<Func<SomeEntity, bool>> AsExpression()
    {
        return entity => entity.ChildEntities.Any(x => x.Value == value);
    }
}
