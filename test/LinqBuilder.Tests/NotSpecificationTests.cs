using LinqBuilder.Tests.TestHelpers;
using Shouldly;
using Xunit;

namespace LinqBuilder.Tests
{
    public class NotSpecificationTests
    {
        [Theory]
        [ClassData(typeof(TestData))]
        public void IsSatisfiedBy_Theory(Entity entity, bool expected)
        {
            new Value1Specification(5)
                .Not()
                .IsSatisfiedBy(entity)
                .ShouldBe(expected);
        }

        private class TestData : EntityTheoryData
        {
            public TestData()
            {
                AddEntity(3, 1, true);
                AddEntity(5, 1, false);
            }
        }
    }
}
