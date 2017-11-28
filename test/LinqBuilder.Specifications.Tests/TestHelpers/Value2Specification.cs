using System;
using System.Linq.Expressions;

namespace LinqBuilder.Specifications.Tests.TestHelpers
{
    public class Value2Specification : Specification<TestEntity>
    {
        private readonly int _value;

        public Value2Specification(int value)
        {
            _value = value;
        }

        public override Expression<Func<TestEntity, bool>> AsExpression()
        {
            return entity => entity.Value2 == _value;
        }
    }
}
