using System;
using System.Linq.Expressions;

namespace LinqBuilder.Testing.Specifications
{
    public class MultipleValueSpecification4 : DynamicSpecification<Entity, int, int, int, int>
    {
        public MultipleValueSpecification4()
        {
        }

        public MultipleValueSpecification4(int value1, int value2, int value3, int value4)
            : base(value1, value2, value3, value4)
        {
        }

        public override Expression<Func<Entity, bool>> AsExpression()
        {
            return entity => entity.Value1 == Value1 &&
                             entity.Value2 == Value2 &&
                             entity.Value3 == Value3 &&
                             entity.Value4 == Value4;
        }
    }
}
