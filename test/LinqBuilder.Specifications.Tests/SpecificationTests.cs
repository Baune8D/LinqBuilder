using System;
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

            var result = specification.Invoke(GetTestList().AsQueryable()).ToList();
            result.Count.ShouldBe(4);
            result.ShouldAllBe(e => e.Value1 == 3);
        }

        [Fact]
        public void Invoke_IEnumerable_ShouldReturnFilteredList()
        {
            var specification = new Value1Specification(3);

            var result = specification.Invoke(GetTestList()).ToList();
            result.Count.ShouldBe(4);
            result.ShouldAllBe(e => e.Value1 == 3);
        }

        [Fact]
        public void Skip_Queryable_ShouldReturn2Entities()
        {
            var specification = new Value1Specification(3).Skip(2);

            var result = specification.Invoke(GetTestList().AsQueryable());
            result.Count().ShouldBe(2);
        }

        [Fact]
        public void Skip_Enumerable_ShouldReturn2Entities()
        {
            var specification = new Value1Specification(3).Skip(2);

            var result = specification.Invoke(GetTestList());
            result.Count().ShouldBe(2);
        }

        [Fact]
        public void Take_Queryable_ShouldReturn3Entities()
        {
            var specification = new Value1Specification(3).Take(3);

            var result = specification.Invoke(GetTestList().AsQueryable());
            result.Count().ShouldBe(3);
        }

        [Fact]
        public void Take_Enumerable_ShouldReturn3Entities()
        {
            var specification = new Value1Specification(3).Take(3);

            var result = specification.Invoke(GetTestList());
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
