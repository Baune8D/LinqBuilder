using System;
using System.Linq.Expressions;
using LinqBuilder.Specifications;

namespace LinqBuilder.IntegrationTests.TestHelpers
{
    public class Value1Specification : Specification<TestData>
    {
        private readonly int _value;

        public Value1Specification(int value)
        {
            _value = value;
        }

        public override Expression<Func<TestData, bool>> AsExpression()
        {
            return entity => entity.Value1 == _value;
        }
    }
}
