using LinqBuilder.Specifications.Tests.TestHelpers;
using Shouldly;
using Xunit;

namespace LinqBuilder.Specifications.Tests
{
    public class NotSpecificationTests
    {
        [Theory]
        [ClassData(typeof(TestData))]
        public void IsSatisfiedBy_Theory(TestEntity entity, bool expected)
        {
            var specification = new Value1Specification(5).Not();

            specification
                .IsSatisfiedBy(entity)
                .ShouldBe(expected);
        }

        private class TestData : TheoryData<TestEntity, bool>
        {
            public TestData()
            {
                Add(new TestEntity { Value1 = 3 }, true);
                Add(new TestEntity { Value1 = 5 }, false);
            }
        }
    }
}
