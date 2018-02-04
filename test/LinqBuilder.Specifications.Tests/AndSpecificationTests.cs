using LinqBuilder.Specifications.Tests.TestHelpers;
using Shouldly;
using Xunit;

namespace LinqBuilder.Specifications.Tests
{
    public class AndSpecificationTests
    {
        [Theory]
        [ClassData(typeof(TestData))]
        public void IsSatisfiedBy_Theory(TestEntity entity, bool expected)
        {
            var specification = new Value1Specification(3)
                .And(new Value2Specification(5));

            specification
                .IsSatisfiedBy(entity)
                .ShouldBe(expected);
        }

        private class TestData : TheoryData<TestEntity, bool>
        {
            public TestData()
            {
                Add(new TestEntity { Value1 = 3, Value2 = 5 }, true);
                Add(new TestEntity { Value1 = 3, Value2 = 4 }, false);
            }
        }
    }
}
