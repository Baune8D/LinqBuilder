using System;
using System.Collections.Generic;
using System.Linq;
using LinqBuilder.OrderSpecifications.Tests.TestHelpers;
using Shouldly;
using Xunit;

namespace LinqBuilder.OrderSpecifications.Tests
{
    public class LinqExtensionsTests
    {
        [Fact]
        public void OrderBy_IQueryable_ShouldReturnOrderedList()
        {
            var query = GetTestList().AsQueryable();

            var result = query.OrderBy(new Value1OrderSpecification()).ToList();

            result.Count.ShouldBe(3);
            result[0].Value1.ShouldBe(1);
            result[1].Value1.ShouldBe(2);
            result[2].Value1.ShouldBe(3);
        }

        [Fact]
        public void OrderBy_IQueryable_ShouldThrowArgumentNullException()
        {
            var query = GetTestList().AsQueryable();

            Should.Throw<ArgumentNullException>(() => query.OrderBy(null));
        }

        [Fact]
        public void OrderBy_IEnumerable_ShouldReturnOrderedList()
        {
            var query = GetTestList();

            var result = query.OrderBy(new Value1OrderSpecification()).ToList();

            result.Count.ShouldBe(3);
            result[0].Value1.ShouldBe(1);
            result[1].Value1.ShouldBe(2);
            result[2].Value1.ShouldBe(3);
        }

        [Fact]
        public void OrderBy_IEnumerable_ShouldThrowArgumentNullException()
        {
            var collection = GetTestList();

            Should.Throw<ArgumentNullException>(() => collection.OrderBy(null));
        }

        [Fact]
        public void ThenBy_IQueryable_ShouldReturnOrderedList()
        {
            var query = GetTestList().AsQueryable();
            var orderedQuery = query.OrderBy(e => e.Value2);

            var result = orderedQuery.ThenBy(new Value1OrderSpecification()).ToList();

            result.Count.ShouldBe(3);
            result[0].Value1.ShouldBe(1);
            result[1].Value1.ShouldBe(2);
            result[2].Value1.ShouldBe(3);
        }

        [Fact]
        public void ThenBy_IQueryable_ShouldThrowArgumentNullException()
        {
            var query = GetTestList().AsQueryable().OrderBy(e => e.Value2);

            Should.Throw<ArgumentNullException>(() => query.ThenBy(null));
        }

        [Fact]
        public void ThenBy_IEnumerable_ShouldReturnOrderedList()
        {
            var query = GetTestList();
            var orderedQuery = query.OrderBy(e => e.Value2);

            var result = orderedQuery.ThenBy(new Value1OrderSpecification()).ToList();

            result.Count.ShouldBe(3);
            result[0].Value1.ShouldBe(1);
            result[1].Value1.ShouldBe(2);
            result[2].Value1.ShouldBe(3);
        }

        [Fact]
        public void ThenBy_IEnumerable_ShouldThrowArgumentNullException()
        {
            var collection = GetTestList().OrderBy(e => e.Value2);

            Should.Throw<ArgumentNullException>(() => collection.ThenBy(null));
        }

        [Fact]
        public void ExeSpec_IQueryable_ShouldReturnFilteredAndOrderedList()
        {
            var specification = new Value1Specification(2)
                .Or(new Value1Specification(3))
                .OrderBy(new Value1OrderSpecification());

            var query = GetTestList().AsQueryable();

            var result = query.ExeSpec(specification).ToList();

            result.Count.ShouldBe(2);
            result[0].Value1.ShouldBe(2);
            result[1].Value1.ShouldBe(3);
        }

        [Fact]
        public void ExeSpec_IQueryable_ShouldThrowArgumentNullException()
        {
            var query = GetTestList().AsQueryable();

            Should.Throw<ArgumentNullException>(() => query.ExeSpec(null));
        }

        [Fact]
        public void ExeSpec_IEnumerable_ShouldReturnFilteredAndOrderedList()
        {
            var specification = new Value1Specification(2)
                .Or(new Value1Specification(3))
                .OrderBy(new Value1OrderSpecification());

            var query = GetTestList();

            var result = query.ExeSpec(specification).ToList();

            result.Count.ShouldBe(2);
            result[0].Value1.ShouldBe(2);
            result[1].Value1.ShouldBe(3);
        }

        [Fact]
        public void ExeSpec_IEnumerable_ShouldThrowArgumentNullException()
        {
            var collection = GetTestList();

            Should.Throw<ArgumentNullException>(() => collection.ExeSpec(null));
        }

        private static IEnumerable<TestEntity> GetTestList()
        {
            return new List<TestEntity>
            {
                new TestEntity { Value1 = 3 },
                new TestEntity { Value1 = 1 },
                new TestEntity { Value1 = 2 }
            };
        }
    }
}
