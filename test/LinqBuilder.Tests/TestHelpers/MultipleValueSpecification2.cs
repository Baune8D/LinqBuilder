using System;
using System.Linq.Expressions;

namespace LinqBuilder.Tests.TestHelpers
{
    public class MultipleValueSpecification2 : DynamicSpecification<Entity, int, int>
    {
        public MultipleValueSpecification2()
        {
        }

        public MultipleValueSpecification2(int value1, int value2)
            : base(value1, value2)
        {
        }

        public override Expression<Func<Entity, bool>> AsExpression()
        {
            return entity => entity.Value1 == Value1 &&
                             entity.Value2 == Value2;
        }
    }
}
