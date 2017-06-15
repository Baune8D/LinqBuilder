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
        public void OrderBy_IQueryable_ShouldThrowArgumentNullException()
        {
            Should.Throw<ArgumentNullException>(() => GetTestList().AsQueryable().OrderBy(null));
        }

        [Fact]
        public void OrderBy_IEnumerable_ShouldThrowArgumentNullException()
        {
            Should.Throw<ArgumentNullException>(() => GetTestList().OrderBy(null));
        }

        [Fact]
        public void ThenBy_IQueryable_ShouldThrowArgumentNullException()
        {
            Should.Throw<ArgumentNullException>(() => GetTestList().AsQueryable().OrderBy(e => e.Value2).ThenBy(null));
        }

        [Fact]
        public void ThenBy_IEnumerable_ShouldThrowArgumentNullException()
        {
            Should.Throw<ArgumentNullException>(() => GetTestList().OrderBy(e => e.Value2).ThenBy(null));
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
