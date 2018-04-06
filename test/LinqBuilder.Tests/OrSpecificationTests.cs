using LinqBuilder.Tests.TestHelpers;
using Shouldly;
using Xunit;

namespace LinqBuilder.Tests
{
    public class OrSpecificationTests
    {
        [Theory]
        [ClassData(typeof(TestData))]
        public void IsSatisfiedBy_Theory(Entity entity, bool expected)
        {
            new Value1Specification(3)
                .Or(new Value1Specification(5))
                .IsSatisfiedBy(entity)
                .ShouldBe(expected);
        }

        private class TestData : EntityTheoryData
        {
            public TestData()
            {
                AddEntity(3, 1, true);
                AddEntity(5, 1, true);
                AddEntity(4, 1, false);
            }
        }
    }
}
