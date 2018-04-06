using LinqBuilder.Tests.TestHelpers;
using Shouldly;
using Xunit;

namespace LinqBuilder.Tests
{
    public class AndSpecificationTests
    {
        [Theory]
        [ClassData(typeof(TestData))]
        public void IsSatisfiedBy_Theory(Entity entity, bool expected)
        {
            new Value1Specification(3)
                .And(new Value2Specification(5))
                .IsSatisfiedBy(entity)
                .ShouldBe(expected);
        }

        private class TestData : TheoryDataHelper
        {
            public TestData()
            {
                AddEntity(3, 5, true);
                AddEntity(3, 4, false);
            }
        }
    }
}
