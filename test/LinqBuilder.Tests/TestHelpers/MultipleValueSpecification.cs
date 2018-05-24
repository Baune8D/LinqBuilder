using System;
using System.Linq.Expressions;

namespace LinqBuilder.Tests.TestHelpers
{
    public class MultipleValueSpecification : DynamicSpecification<Entity, int, int, int, int>
    {
        public override Expression<Func<Entity, bool>> AsExpression()
        {
            return entity => entity.Value1 == Value1 && 
                             entity.Value2 == Value2 &&
                             entity.Value3 == Value3 &&
                             entity.Value4 == Value4;
        }
    }
}
