using System.Collections.Generic;
using System.Linq;
using LinqBuilder.Specifications.Tests.TestHelpers;
using Shouldly;
using Xunit;

namespace LinqBuilder.Specifications.Tests
{
    public class OrSpecificationTests
    {
        [Fact]
        public void IsSatisfiedBy_NoValueMatching_ShouldReturnFalse()
        {
            var specification = new Value1Specification(3)
                .Or(new Value1Specification(5));

            var entity = new TestEntity { Value1 = 4 };
            specification.IsSatisfiedBy(entity).ShouldBeFalse();
        }

        [Fact]
        public void IsSatisfiedBy_BothValuesMathcing_ShouldReturnTrue()
        {
            var specification = new Value1Specification(3)
                .Or(new Value1Specification(5));

            var entity = new TestEntity { Value1 = 3 };
            specification.IsSatisfiedBy(entity).ShouldBeTrue();

            entity.Value1 = 5;
            specification.IsSatisfiedBy(entity).ShouldBeTrue();
        }

        [Fact]
        public void Skip_IEnumerable_ShouldReturn2Entities()
        {
            var specification = new Value1Specification(3).Skip(2);
            specification = specification.Or(new Value1Specification(5));

            var result = specification.Invoke(GetTestList()).ToList();
            result.Count.ShouldBe(2);
            result.ShouldAllBe(e => e.Value1 == 3 || e.Value1 == 5);
        }

        [Fact]
        public void Take_IEnumerable_ShouldReturn2Entities()
        {
            var specification = new Value1Specification(3).Take(2);
            specification = specification.Or(new Value2Specification(5));

            var result = specification.Invoke(GetTestList()).ToList();
            result.Count.ShouldBe(2);
            result.ShouldAllBe(e => e.Value1 == 3 || e.Value1 == 5);
        }

        private static IEnumerable<TestEntity> GetTestList()
        {
            return new List<TestEntity>
            {
                new TestEntity { Value1 = 3 },
                new TestEntity { Value1 = 3 },
                new TestEntity { Value1 = 2 },
                new TestEntity { Value1 = 5 },
                new TestEntity { Value1 = 3 }
            };
        }
    }
}
