using System;
using System.Linq.Expressions;

namespace LinqBuilder.Tests.TestHelpers
{
    public class Value1Specification : DynamicSpecification<Entity, int>
    {
        public override Expression<Func<Entity, bool>> AsExpression()
        {
            return entity => entity.Value1 == Value;
        }
    }
}
