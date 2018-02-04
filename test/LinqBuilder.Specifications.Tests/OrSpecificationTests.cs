using System;
using LinqBuilder.Specifications.Tests.TestHelpers;
using Shouldly;
using Xunit;

namespace LinqBuilder.Specifications.Tests
{
    public class OrSpecificationTests
    {
        [Theory]
        [ClassData(typeof(TestData))]
        public void IsSatisfiedBy_Theory(TestEntity entity, bool expected)
        {
            var specification = new Value1Specification(3)
                .Or(new Value1Specification(5));

            specification
                .IsSatisfiedBy(entity)
                .ShouldBe(expected);
        }

        [Fact]
        public void Where_IQueryable_ShouldThrowArgumentNullException()
        {
            var specification = new Value1Specification(3)
                .Or(new Value1Specification(5));

            Should.Throw<ArgumentNullException>(() => _fixture.TestQuery.Where(null));
        }

        private class TestData : TheoryData<TestEntity, bool>
        {
            public TestData()
            {
                Add(new TestEntity { Value1 = 3 }, true);
                Add(new TestEntity { Value1 = 5 }, true);
                Add(new TestEntity { Value1 = 4 }, false);
            }
        }
    }
}
