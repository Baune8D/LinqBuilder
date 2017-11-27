using System.Collections.Generic;
using System.Linq;
using LinqBuilder.OrderSpecifications.Tests.TestHelpers;
using Shouldly;
using Xunit;

namespace LinqBuilder.OrderSpecifications.Tests
{
    public class SpecificationExtensionsTests
    {
        [Fact]
        public void OrderBy_IQueryable_ShouldReturnFilteredAndOrderedList()
        {
            var specification = new Value1Specification(2)
                .Or(new Value1Specification(3))
                .OrderBy(new Value1OrderSpecification());

            var query = GetTestList().AsQueryable();

            var result = specification.Invoke(query).ToList();

            result.Count.ShouldBe(3);
            result[0].Value1.ShouldBe(2);
            result[1].Value1.ShouldBe(2);
            result[2].Value1.ShouldBe(3);
        }

        [Fact]
        public void OrderBy_IEnumerable_ShouldReturnFilteredAndOrderedList()
        {
            var specification = new Value1Specification(2)
                .Or(new Value1Specification(3))
                .OrderBy(new Value1OrderSpecification());

            var query = GetTestList();

            var result = specification.Invoke(query).ToList();

            result.Count.ShouldBe(3);
            result[0].Value1.ShouldBe(2);
            result[1].Value1.ShouldBe(2);
            result[2].Value1.ShouldBe(3);
        }

        private static IEnumerable<TestEntity> GetTestList()
        {
            return new List<TestEntity>
            {
                new TestEntity { Value1 = 2 },
                new TestEntity { Value1 = 3 },
                new TestEntity { Value1 = 2 },
                new TestEntity { Value1 = 1 }
            };
        }
    }
}
