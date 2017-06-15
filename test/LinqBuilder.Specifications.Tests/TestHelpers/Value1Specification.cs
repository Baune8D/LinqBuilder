using System;
using System.Linq.Expressions;

namespace LinqBuilder.Specifications.Tests.TestHelpers
{
    public class Value1Specification : Specification<TestEntity>
    {
        private readonly int _shouldBe;

        public Value1Specification(int shouldBe)
        {
            _shouldBe = shouldBe;
        }

        public override Expression<Func<TestEntity, bool>> AsExpression()
        {
            return entity => entity.Value1 == _shouldBe;
        }
    }
}
