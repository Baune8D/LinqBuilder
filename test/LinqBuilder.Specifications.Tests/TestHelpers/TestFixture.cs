using System.Collections.Generic;
using System.Linq;

namespace LinqBuilder.Specifications.Tests.TestHelpers
{
    public class TestFixture
    {
        public readonly int Value = 3;

        public Value1Specification Specification => new Value1Specification(Value);

        public IEnumerable<TestEntity> TestCollection => new List<TestEntity>
        {
            new TestEntity { Value1 = Value },
            new TestEntity { Value1 = Value },
            new TestEntity { Value1 = 2 }
        };

        public IQueryable<TestEntity> TestQuery => TestCollection.AsQueryable();
    }
}
