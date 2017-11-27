using System.Collections.Generic;
using System.Linq;
using LinqBuilder.Specifications.Tests.TestHelpers;
using Shouldly;
using Xunit;

namespace LinqBuilder.Specifications.Tests
{
    public class SpecificationTests
    {
        [Fact]
        public void IsSatisfiedBy_DefaltValue_ShouldReturnTrue()
        {
            var specification = new Specification<TestEntity>();
            var entity = new TestEntity();

            specification.IsSatisfiedBy(entity).ShouldBeTrue();
        }

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
        public void Invoke_IQueryable_ShouldReturnFilteredList()
        {
            var specification = new Value1Specification(3);
            var query = GetTestList().AsQueryable();

            var result = specification.Invoke(query).ToList();

            result.Count.ShouldBe(2);
            result.ShouldAllBe(e => e.Value1 == 3);
        }

        [Fact]
        public void Invoke_IEnumerable_ShouldReturnFilteredList()
        {
            var specification = new Value1Specification(3);
            var collection = GetTestList();

            var result = specification.Invoke(collection).ToList();

            result.Count.ShouldBe(2);
            result.ShouldAllBe(e => e.Value1 == 3);
        }

        private static IEnumerable<TestEntity> GetTestList()
        {
            return new List<TestEntity>
            {
                new TestEntity { Value1 = 3 },
                new TestEntity { Value1 = 3 },
                new TestEntity { Value1 = 2 }
            };
        }
    }
}
