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
            var specification = new Value1Specification(3)
                .Or(new Value1Specification(5));

            specification
                .IsSatisfiedBy(entity)
                .ShouldBe(expected);
        }

        private class TestData : TheoryData<Entity, bool>
        {
            public TestData()
            {
                Add(new Entity { Value1 = 3 }, true);
                Add(new Entity { Value1 = 5 }, true);
                Add(new Entity { Value1 = 4 }, false);
            }
        }
    }
}
