using System;
using System.Linq.Expressions;

namespace LinqBuilder.Filtering.Tests.TestHelpers
{
    public class Value2Specification : CompositeSpecification<TestEntity>
    {
        private readonly int _shouldBe;

        public Value2Specification(int shouldBe)
        {
            _shouldBe = shouldBe;
        }

        public override Expression<Func<TestEntity, bool>> AsExpression()
        {
            return entity => entity.Value2 == _shouldBe;
        }
    }
}
