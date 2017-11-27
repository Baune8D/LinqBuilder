using System;
using System.Collections.Generic;
using System.Linq;
using LinqBuilder.Specifications.Tests.TestHelpers;
using Shouldly;
using Xunit;

namespace LinqBuilder.Specifications.Tests
{
    public class LinqExtensionsTests
    {
        [Fact]
        public void Where_IQueryable_ShouldReturn1Result()
        {
            var query = GetTestList().AsQueryable();

            var result = query.Where(new Value1Specification(5)).ToList();

            result.Count.ShouldBe(1);
            result[0].Value1.ShouldBe(5);
        }

        [Fact]
        public void Where_IQueryable_ShouldThrowArgumentNullException()
        {
            var query = GetTestList().AsQueryable();

            Should.Throw<ArgumentNullException>(() => query.Where(null));
        }

        [Fact]
        public void Where_IEnumerable_ShouldReturn1Result()
        {
            var query = GetTestList();

            var result = query.Where(new Value1Specification(5)).ToList();

            result.Count.ShouldBe(1);
            result[0].Value1.ShouldBe(5);
        }

        [Fact]
        public void Where_IEnumerable_ShouldThrowArgumentNullException()
        {
            var collection = GetTestList();

            Should.Throw<ArgumentNullException>(() => collection.Where(null));
        }

        [Fact]
        public void ExeSpec_IQueryable_ShouldReturn1Result()
        {
            var query = GetTestList().AsQueryable();

            var result = query.ExeSpec(new Value1Specification(5)).ToList();

            result.Count.ShouldBe(1);
            result[0].Value1.ShouldBe(5);
        }

        [Fact]
        public void ExeSpec_IQueryable_ShouldThrowArgumentNullException()
        {
            var query = GetTestList().AsQueryable();

            Should.Throw<ArgumentNullException>(() => query.ExeSpec(null));
        }

        [Fact]
        public void ExeSpec_IEnumerable_ShouldReturn1Result()
        {
            var query = GetTestList();

            var result = query.ExeSpec(new Value1Specification(5)).ToList();

            result.Count.ShouldBe(1);
            result[0].Value1.ShouldBe(5);
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
                new TestEntity { Value1 = 4 },
                new TestEntity { Value1 = 5 },
                new TestEntity { Value1 = 4 }
            };
        }
    }
}
