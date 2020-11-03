using System;
using System.Linq.Expressions;

namespace LinqBuilder.Testing.Specifications
{
    public class MultipleValueSpecification3 : DynamicSpecification<Entity, int, int, int>
    {
        public MultipleValueSpecification3()
        {
        }

        public MultipleValueSpecification3(int value1, int value2, int value3)
            : base(value1, value2, value3)
        {
        }

        public override Expression<Func<Entity, bool>> AsExpression()
        {
            return entity => entity.Value1 == Value1 &&
                             entity.Value2 == Value2 &&
                             entity.Value3 == Value3;
        }
    }
}
