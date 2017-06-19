using System.Collections.Generic;
using System.Linq;
using LinqBuilder.Specifications.Tests.TestHelpers;
using Shouldly;
using Xunit;

namespace LinqBuilder.Specifications.Tests
{
    public class AndSpecificationTests
    {
        [Fact]
        public void IsSatisfiedBy_OneWrongValue_ShouldReturnFalse()
        {
            var specification = new Value1Specification(3)
                .And(new Value2Specification(5));

            var entity = new TestEntity
            {
                Value1 = 3,
                Value2 = 4
            };
            specification.IsSatisfiedBy(entity).ShouldBeFalse();
        }

        [Fact]
        public void IsSatisfiedBy_TwoCorrectValues_ShouldReturnTrue()
        {
            var specification = new Value1Specification(3)
                .And(new Value2Specification(5));

            var entity = new TestEntity
            {
                Value1 = 3,
                Value2 = 5
            };
            specification.IsSatisfiedBy(entity).ShouldBeTrue();
        }

        [Fact]
        public void Skip_IEnumerable_ShouldReturn2Entities()
        {
            var specification = new Value1Specification(3).Skip(2);
            specification = specification.And(new Value2Specification(5));

            var result = specification.Invoke(GetTestList()).ToList();
            result.Count.ShouldBe(2);
            result.ShouldAllBe(e => e.Value1 == 3);
            result.ShouldAllBe(e => e.Value2 == 5);
        }

        [Fact]
        public void Take_IEnumerable_ShouldReturn2Entities()
        {
            var specification = new Value1Specification(3).Take(2);
            specification = specification.And(new Value2Specification(5));

            var result = specification.Invoke(GetTestList()).ToList();
            result.Count.ShouldBe(2);
            result.ShouldAllBe(e => e.Value1 == 3);
            result.ShouldAllBe(e => e.Value2 == 5);
        }

        private static IEnumerable<TestEntity> GetTestList()
        {
            return new List<TestEntity>
            {
                new TestEntity { Value1 = 3, Value2 = 5 },
                new TestEntity { Value1 = 3, Value2 = 1 },
                new TestEntity { Value1 = 3, Value2 = 5 },
                new TestEntity { Value1 = 3, Value2 = 5 },
                new TestEntity { Value1 = 3, Value2 = 5 }
            };
        }
    }
}
