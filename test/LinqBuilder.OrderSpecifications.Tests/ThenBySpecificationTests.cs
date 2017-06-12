using System.Collections.Generic;
using System.Linq;
using LinqBuilder.Ordering.Tests.TestHelpers;
using Shouldly;
using Xunit;

namespace LinqBuilder.Ordering.Tests
{
    public class ThenBySpecificationTests
    {
        [Fact]
        public void Invoke_QueryableAscending_ShouldReturnOrderedList()
        {
            var specification = new Value1OrderSpecification()
                .ThenBy(new Value2OrderSpecification());

            var result = specification.Invoke(GetTestList().AsQueryable()).ToList();
            result[0].Value1.ShouldBe(1);
            result[1].Value1.ShouldBe(1);
            result[2].Value1.ShouldBe(2);
            result[2].Value2.ShouldBe(1);
            result[3].Value1.ShouldBe(2);
            result[3].Value2.ShouldBe(2);
            result[4].Value1.ShouldBe(3);
        }

        [Fact]
        public void Invoke_EnumerableAscending_ShouldReturnOrderedList()
        {
            var specification = new Value1OrderSpecification()
                .ThenBy(new Value2OrderSpecification());

            var result = specification.Invoke(GetTestList()).ToList();
            result[0].Value1.ShouldBe(1);
            result[1].Value1.ShouldBe(1);
            result[2].Value1.ShouldBe(2);
            result[2].Value2.ShouldBe(1);
            result[3].Value1.ShouldBe(2);
            result[3].Value2.ShouldBe(2);
            result[4].Value1.ShouldBe(3);
        }

        [Fact]
        public void Invoke_QueryableDescending_ShouldReturnOrderedList()
        {
            var specification = new Value1OrderSpecification(Order.Descending)
                .ThenBy(new Value2OrderSpecification(Order.Descending));

            var result = specification.Invoke(GetTestList().AsQueryable()).ToList();
            result[0].Value1.ShouldBe(3);
            result[1].Value1.ShouldBe(2);
            result[1].Value2.ShouldBe(2);
            result[2].Value1.ShouldBe(2);
            result[2].Value2.ShouldBe(1);
            result[3].Value1.ShouldBe(1);
            result[4].Value1.ShouldBe(1);
        }

        [Fact]
        public void Invoke_EnumerableDescending_ShouldReturnOrderedList()
        {
            var specification = new Value1OrderSpecification(Order.Descending)
                .ThenBy(new Value2OrderSpecification(Order.Descending));

            var result = specification.Invoke(GetTestList()).ToList();
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

            var result = specification.Invoke(GetTestList()).ToList();
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
