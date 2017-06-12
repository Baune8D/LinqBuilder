using System.Collections.Generic;
using System.Linq;
using LinqBuilder.Filtering.Tests.TestHelpers;
using Shouldly;
using Xunit;

namespace LinqBuilder.Filtering.Tests
{
    public class CompositeSpecificationTests
    {
        [Fact]
        public void IsSatisfiedBy_WrongValue_ShouldReturnFalse()
        {
            var specification = new Value1Specification(5);

            var entity = new TestEntity { Value1 = 4 };
            specification.IsSatisfiedBy(entity).ShouldBeFalse();
        }

        [Fact]
        public void IsSatisfiedBy_CorrectValue_ShouldReturnTrue()
        {
            var specification = new Value1Specification(5);

            var entity = new TestEntity { Value1 = 5 };
            specification.IsSatisfiedBy(entity).ShouldBeTrue();
        }

        [Fact]
        public void Skip_ListOf5Entities_ShouldReturn2Entities()
        {
            var specification = new Value1Specification(3).Skip(2);

            var result = specification.Invoke(GetTestList().AsQueryable());
            result.Count().ShouldBe(2);
        }

        [Fact]
        public void Take_ListOf5Entities_ShouldReturn3Entities()
        {
            var specification = new Value1Specification(3).Take(3);

            var result = specification.Invoke(GetTestList().AsQueryable());
            result.Count().ShouldBe(3);
        }

        private static IEnumerable<TestEntity> GetTestList()
        {
            return new List<TestEntity>
            {
                new TestEntity { Value1 = 3 },
                new TestEntity { Value1 = 3 },
                new TestEntity { Value1 = 2 },
                new TestEntity { Value1 = 3 },
                new TestEntity { Value1 = 3 }
            };
        }
    }
}
