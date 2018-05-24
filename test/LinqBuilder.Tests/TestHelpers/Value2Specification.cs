using System;
using System.Linq.Expressions;

namespace LinqBuilder.Tests.TestHelpers
{
    public class Value2Specification : DynamicSpecification<Entity, int>
    {
        public override Expression<Func<Entity, bool>> AsExpression()
        {
            return entity => entity.Value2 == Value;
        }
    }
}
