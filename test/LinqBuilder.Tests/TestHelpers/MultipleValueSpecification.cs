using System;
using System.Linq.Expressions;

namespace LinqBuilder.Tests.TestHelpers
{
    public class MultipleValueSpecification1 : DynamicSpecification<Entity, int>
    {
        public MultipleValueSpecification1() { }

        public MultipleValueSpecification1(int value1) : base(value1) { }

        public override Expression<Func<Entity, bool>> AsExpression()
        {
            return entity => entity.Value1 == Value;
        }
    }

    public class MultipleValueSpecification2 : DynamicSpecification<Entity, int, int>
    {
        public MultipleValueSpecification2() { }

        public MultipleValueSpecification2(int value1, int value2) : base(value1, value2) { }

        public override Expression<Func<Entity, bool>> AsExpression()
        {
            return entity => entity.Value1 == Value1 &&
                             entity.Value2 == Value2;
        }
    }

    public class MultipleValueSpecification3 : DynamicSpecification<Entity, int, int, int>
    {
        public MultipleValueSpecification3() { }

        public MultipleValueSpecification3(int value1, int value2, int value3) : base(value1, value2, value3) { }

        public override Expression<Func<Entity, bool>> AsExpression()
        {
            return entity => entity.Value1 == Value1 &&
                             entity.Value2 == Value2 &&
                             entity.Value3 == Value3;
        }
    }

    public class MultipleValueSpecification4 : DynamicSpecification<Entity, int, int, int, int>
    {
        public MultipleValueSpecification4() { }

        public MultipleValueSpecification4(int value1, int value2, int value3, int value4) : base(value1, value2, value3, value4) { }

        public override Expression<Func<Entity, bool>> AsExpression()
        {
            return entity => entity.Value1 == Value1 && 
                             entity.Value2 == Value2 &&
                             entity.Value3 == Value3 &&
                             entity.Value4 == Value4;
        }
    }
}
