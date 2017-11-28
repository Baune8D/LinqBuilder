using System.Collections.Generic;
using System.Linq;
using LinqBuilder.OrderSpecifications.Tests.TestHelpers;
using Shouldly;
using Xunit;

namespace LinqBuilder.OrderSpecifications.Tests
{
    public class OrderSpecificationTests
    {
        [Fact]
        public void Invoke_IQueryableAscending_ShouldReturnOrderedList()
        {
            var specification = new Value1OrderSpecification();
            var query = GetTestList().AsQueryable();

            var result = specification.Invoke(query).ToList();

            result[0].Value1.ShouldBe(1);
            result[1].Value1.ShouldBe(2);
            result[2].Value1.ShouldBe(3);
        }

        [Fact]
        public void Invoke_IEnumerableAscending_ShouldReturnOrderedList()
        {
            var specification = new Value1OrderSpecification();
            var collection = GetTestList();

            var result = specification.Invoke(collection).ToList();

            result[0].Value1.ShouldBe(1);
            result[1].Value1.ShouldBe(2);
            result[2].Value1.ShouldBe(3);
        }

        [Fact]
        public void Invoke_IQueryableDescending_ShouldReturnOrderedList()
        {
            var specification = new Value1OrderSpecification(Order.Descending);
            var query = GetTestList().AsQueryable();

            var result = specification.Invoke(query).ToList();

            result[0].Value1.ShouldBe(3);
            result[1].Value1.ShouldBe(2);
            result[2].Value1.ShouldBe(1);
        }

        [Fact]
        public void Invoke_IEnumerableDescending_ShouldReturnOrderedList()
        {
            var specification = new Value1OrderSpecification(Order.Descending);
            var collection = GetTestList();

            var result = specification.Invoke(collection).ToList();

            result[0].Value1.ShouldBe(3);
            result[1].Value1.ShouldBe(2);
            result[2].Value1.ShouldBe(1);
        }

        [Fact]
        public void Skip_IQueryable_ShouldReturn2Entities()
        {
            var specification = new Value1OrderSpecification().Skip(1);
            var collection = GetTestList().AsQueryable();

            var result = specification.Invoke(collection).ToList();

            result.Count.ShouldBe(2);
            result[0].Value1.ShouldBe(2);
            result[1].Value1.ShouldBe(3);
        }

        [Fact]
        public void Skip_IEnumerable_ShouldReturn2Entities()
        {
            var specification = new Value1OrderSpecification().Skip(1);
            var collection = GetTestList();

            var result = specification.Invoke(collection).ToList();

            result.Count.ShouldBe(2);
            result[0].Value1.ShouldBe(2);
            result[1].Value1.ShouldBe(3);
        }

        [Fact]
        public void Take_IQueryable_ShouldReturn2Entities()
        {
            var specification = new Value1OrderSpecification().Take(2);
            var collection = GetTestList().AsQueryable();

            var result = specification.Invoke(collection).ToList();

            result.Count.ShouldBe(2);
            result[0].Value1.ShouldBe(1);
            result[1].Value1.ShouldBe(2);
        }

        [Fact]
        public void Take_IEnumerable_ShouldReturn2Entities()
        {
            var specification = new Value1OrderSpecification().Take(2);
            var collection = GetTestList();

            var result = specification.Invoke(collection).ToList();

            result.Count.ShouldBe(2);
            result[0].Value1.ShouldBe(1);
            result[1].Value1.ShouldBe(2);
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
