using System.Collections.Generic;
using System.Linq;
using LinqBuilder.OrderSpecifications.Tests.TestHelpers;
using Shouldly;
using Xunit;

namespace LinqBuilder.OrderSpecifications.Tests
{
    public class CompositeOrderSpecificationTests
    {
        [Fact]
        public void Invoke_Queryable_ShouldReturnOrderedList()
        {
            var specification = new Value1OrderSpecification()
                .ThenBy(new Value2OrderSpecification());

            var query = GetTestList().AsQueryable();

            var result = specification.Invoke(query).ToList();
            result[0].Value1.ShouldBe(1);
            result[1].Value1.ShouldBe(1);
            result[2].Value1.ShouldBe(2);
            result[2].Value2.ShouldBe(1);
            result[3].Value1.ShouldBe(2);
            result[3].Value2.ShouldBe(2);
            result[4].Value1.ShouldBe(3);
        }

        [Fact]
        public void Invoke_Enumerable_ShouldReturnOrderedList()
        {
            var specification = new Value1OrderSpecification()
                .ThenBy(new Value2OrderSpecification());

            var collection = GetTestList();

            var result = specification.Invoke(collection).ToList();
            result[0].Value1.ShouldBe(1);
            result[1].Value1.ShouldBe(1);
            result[2].Value1.ShouldBe(2);
            result[2].Value2.ShouldBe(1);
            result[3].Value1.ShouldBe(2);
            result[3].Value2.ShouldBe(2);
            result[4].Value1.ShouldBe(3);
        }

        [Fact]
        public void Invoke_OrderedQueryable_ShouldReturnOrderedList()
        {
            var specification = new Value1OrderSpecification(Order.Descending)
                .ThenBy(new Value2OrderSpecification(Order.Descending));

            var query = GetTestList().AsQueryable().OrderBy(e => e.Value3);

            var result = specification.Invoke(query).ToList();
            result[0].Value1.ShouldBe(3);
            result[1].Value1.ShouldBe(2);
            result[1].Value2.ShouldBe(2);
            result[2].Value1.ShouldBe(2);
            result[2].Value2.ShouldBe(1);
            result[3].Value1.ShouldBe(1);
            result[4].Value1.ShouldBe(1);
        }

        [Fact]
        public void Invoke_OrderedEnumerable_ShouldReturnOrderedList()
        {
            var specification = new Value1OrderSpecification(Order.Descending)
                .ThenBy(new Value2OrderSpecification(Order.Descending));

            var collection = GetTestList().OrderBy(e => e.Value3);

            var result = specification.Invoke(collection).ToList();
            result[0].Value1.ShouldBe(3);
            result[1].Value1.ShouldBe(2);
            result[1].Value2.ShouldBe(2);
            result[2].Value1.ShouldBe(2);
            result[2].Value2.ShouldBe(1);
            result[3].Value1.ShouldBe(1);
            result[4].Value1.ShouldBe(1);
        }

        [Fact]
        public void Invoke_ThreeOrderSpecifications_ShouldReturnOrderedList()
        {
            var specification = new Value1OrderSpecification()
                .ThenBy(new Value2OrderSpecification())
                .ThenBy(new Value3OrderSpecification());

            var collection = GetTestList();

            var result = specification.Invoke(collection).ToList();
            result[0].Value1.ShouldBe(1);
            result[0].Value3.ShouldBe(1);
            result[1].Value1.ShouldBe(1);
            result[1].Value3.ShouldBe(2);
            result[2].Value1.ShouldBe(2);
            result[2].Value2.ShouldBe(1);
            result[3].Value1.ShouldBe(2);
            result[3].Value2.ShouldBe(2);
            result[4].Value1.ShouldBe(3);
        }

        [Fact]
        public void Skip_IQueryable_ShouldReturn2Entities()
        {
            var specification = new Value1OrderSpecification()
                .ThenBy(new Value2OrderSpecification())
                .Skip(2);

            var collection = GetTestList().AsQueryable();

            var result = specification.Invoke(collection).ToList();

            result.Count.ShouldBe(3);
            result[0].Value1.ShouldBe(2);
            result[1].Value1.ShouldBe(2);
            result[2].Value1.ShouldBe(3);
        }

        [Fact]
        public void Skip_IEnumerable_ShouldReturn2Entities()
        {
            var specification = new Value1OrderSpecification()
                .ThenBy(new Value2OrderSpecification())
                .Skip(2);

            var collection = GetTestList();

            var result = specification.Invoke(collection).ToList();

            result.Count.ShouldBe(3);
            result[0].Value1.ShouldBe(2);
            result[1].Value1.ShouldBe(2);
            result[2].Value1.ShouldBe(3);
        }

        [Fact]
        public void Take_IQueryable_ShouldReturn2Entities()
        {
            var specification = new Value1OrderSpecification()
                .ThenBy(new Value2OrderSpecification())
                .Take(2);

            var collection = GetTestList().AsQueryable();

            var result = specification.Invoke(collection).ToList();

            result.Count.ShouldBe(2);
            result[0].Value1.ShouldBe(1);
            result[1].Value1.ShouldBe(1);
        }

        [Fact]
        public void Take_IEnumerable_ShouldReturn2Entities()
        {
            var specification = new Value1OrderSpecification()
                .ThenBy(new Value2OrderSpecification())
                .Take(2);

            var collection = GetTestList();

            var result = specification.Invoke(collection).ToList();

            result.Count.ShouldBe(2);
            result[0].Value1.ShouldBe(1);
            result[1].Value1.ShouldBe(1);
        }

        private static IEnumerable<TestEntity> GetTestList()
        {
            return new List<TestEntity>
            {
                new TestEntity { Value1 = 3 },
                new TestEntity { Value1 = 1, Value3 = 2 },
                new TestEntity { Value1 = 2, Value2 = 1 },
                new TestEntity { Value1 = 2, Value2 = 2 },
                new TestEntity { Value1 = 1, Value3 = 1 }
            };
        }
    }
}
