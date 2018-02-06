using System.Linq;
using LinqBuilder.OrderSpecifications.Tests.TestHelpers;
using Shouldly;
using Xunit;

namespace LinqBuilder.OrderSpecifications.Tests
{
    public class OrderSpecificationTests : IClassFixture<Fixture>
    {
        private readonly Fixture _fixture;

        public OrderSpecificationTests(Fixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Invoke_IQueryableAscending_ShouldReturnOrderedList()
        {
            var specification = new Value1OrderSpecification();

            var result = specification.Invoke(_fixture.Query).ToList();

            result[0].Value1.ShouldBe(1);
            result[1].Value1.ShouldBe(2);
            result[2].Value1.ShouldBe(3);
        }

        [Fact]
        public void Invoke_IEnumerableAscending_ShouldReturnOrderedList()
        {
            var specification = new Value1OrderSpecification();

            var result = specification.Invoke(_fixture.Collection).ToList();

            result[0].Value1.ShouldBe(1);
            result[1].Value1.ShouldBe(2);
            result[2].Value1.ShouldBe(3);
        }

        [Fact]
        public void Invoke_IQueryableDescending_ShouldReturnOrderedList()
        {
            var specification = new Value1OrderSpecification(Order.Descending);

            var result = specification.Invoke(_fixture.Query).ToList();

            result[0].Value1.ShouldBe(3);
            result[1].Value1.ShouldBe(2);
            result[2].Value1.ShouldBe(1);
        }

        [Fact]
        public void Invoke_IEnumerableDescending_ShouldReturnOrderedList()
        {
            var specification = new Value1OrderSpecification(Order.Descending);

            var result = specification.Invoke(_fixture.Collection).ToList();

            result[0].Value1.ShouldBe(3);
            result[1].Value1.ShouldBe(2);
            result[2].Value1.ShouldBe(1);
        }

        [Fact]
        public void Skip_IQueryable_ShouldReturn2Entities()
        {
            var specification = new Value1OrderSpecification().Skip(1);

            var result = specification.Invoke(_fixture.Query).ToList();

            result.Count.ShouldBe(2);
            result[0].Value1.ShouldBe(2);
            result[1].Value1.ShouldBe(3);
        }

        [Fact]
        public void Skip_IEnumerable_ShouldReturn2Entities()
        {
            var specification = new Value1OrderSpecification().Skip(1);

            var result = specification.Invoke(_fixture.Collection).ToList();

            result.Count.ShouldBe(2);
            result[0].Value1.ShouldBe(2);
            result[1].Value1.ShouldBe(3);
        }

        [Fact]
        public void Take_IQueryable_ShouldReturn2Entities()
        {
            var specification = new Value1OrderSpecification().Take(2);

            var result = specification.Invoke(_fixture.Query).ToList();

            result.Count.ShouldBe(2);
            result[0].Value1.ShouldBe(1);
            result[1].Value1.ShouldBe(2);
        }

        [Fact]
        public void Take_IEnumerable_ShouldReturn2Entities()
        {
            var specification = new Value1OrderSpecification().Take(2);

            var result = specification.Invoke(_fixture.Collection).ToList();

            result.Count.ShouldBe(2);
            result[0].Value1.ShouldBe(1);
            result[1].Value1.ShouldBe(2);
        }
    }
}
