using System.Linq;
using LinqBuilder.OrderBy.Tests.TestHelpers;
using Shouldly;
using Xunit;

namespace LinqBuilder.OrderBy.Tests
{
    public class OrderedSpecificationTests
    {
        private readonly Fixture _fixture;

        public OrderedSpecificationTests()
        {
            _fixture = new Fixture();
            _fixture.AddToCollection(3, 1, 1);
            _fixture.AddToCollection(1, 2, 1);
            _fixture.AddToCollection(2, 2, 2);
            _fixture.AddToCollection(2, 1, 1);
            _fixture.AddToCollection(2, 2, 1);
        }

        [Fact]
        public void GetSpecification_OrderedSpecification_ShouldReturnSpecification()
        {
            var specification = new Value1Specification(1);

            specification.OrderBy(new Value1OrderSpecification()).GetSpecification().ShouldBe(specification);
        }

        [Fact]
        public void Invoke_Queryable_ShouldReturnOrderedList()
        {
            var specification = new Value1OrderSpecification()
                .ThenBy(new Value2OrderSpecification())
                .ThenBy(new Value3OrderSpecification());

            var result = specification.Invoke(_fixture.Query).ToList();

            result[0].Value1.ShouldBe(1);
            result[1].Value1.ShouldBe(2);
            result[1].Value2.ShouldBe(1);
            result[2].Value1.ShouldBe(2);
            result[2].Value2.ShouldBe(2);
            result[2].Value3.ShouldBe(1);
            result[3].Value1.ShouldBe(2);
            result[3].Value2.ShouldBe(2);
            result[3].Value3.ShouldBe(2);
            result[4].Value1.ShouldBe(3);
        }

        [Fact]
        public void Invoke_Enumerable_ShouldReturnOrderedList()
        {
            var specification = new Value1OrderSpecification()
                .ThenBy(new Value2OrderSpecification())
                .ThenBy(new Value3OrderSpecification());

            var result = specification.Invoke(_fixture.Collection).ToList();

            result[0].Value1.ShouldBe(1);
            result[1].Value1.ShouldBe(2);
            result[1].Value2.ShouldBe(1);
            result[2].Value1.ShouldBe(2);
            result[2].Value2.ShouldBe(2);
            result[2].Value3.ShouldBe(1);
            result[3].Value1.ShouldBe(2);
            result[3].Value2.ShouldBe(2);
            result[3].Value3.ShouldBe(2);
            result[4].Value1.ShouldBe(3);
        }

        [Fact]
        public void Skip_IQueryable_ShouldReturn2Entities()
        {
            var specification = new Value1OrderSpecification()
                .ThenBy(new Value2OrderSpecification())
                .Skip(3);

            var result = specification.Invoke(_fixture.Query).ToList();

            result.Count.ShouldBe(2);
            result[0].Value1.ShouldBe(2);
            result[0].Value2.ShouldBe(2);
            result[0].Value3.ShouldBe(1);
            result[1].Value1.ShouldBe(3);
        }

        [Fact]
        public void Skip_IEnumerable_ShouldReturn2Entities()
        {
            var specification = new Value1OrderSpecification()
                .ThenBy(new Value2OrderSpecification())
                .Skip(3);

            var result = specification.Invoke(_fixture.Collection).ToList();

            result.Count.ShouldBe(2);
            result[0].Value1.ShouldBe(2);
            result[0].Value2.ShouldBe(2);
            result[0].Value3.ShouldBe(1);
            result[1].Value1.ShouldBe(3);
        }

        [Fact]
        public void Take_IQueryable_ShouldReturn2Entities()
        {
            var specification = new Value1OrderSpecification()
                .ThenBy(new Value2OrderSpecification())
                .Take(2);

            var result = specification.Invoke(_fixture.Query).ToList();

            result.Count.ShouldBe(2);
            result[0].Value1.ShouldBe(1);
            result[1].Value1.ShouldBe(2);
            result[1].Value2.ShouldBe(1);
        }

        [Fact]
        public void Take_IEnumerable_ShouldReturn2Entities()
        {
            var specification = new Value1OrderSpecification()
                .ThenBy(new Value2OrderSpecification())
                .Take(2);

            var result = specification.Invoke(_fixture.Collection).ToList();

            result.Count.ShouldBe(2);
            result[0].Value1.ShouldBe(1);
            result[1].Value1.ShouldBe(2);
            result[1].Value2.ShouldBe(1);
        }

        [Fact]
        public void Paginate_PageNoAndSize_ShouldReturnCorrectOrdering()
        {
            var specification = new Value1OrderSpecification()
                .ThenBy(new Value2OrderSpecification())
                .Paginate(2, 5);

            var ordering = specification.GetOrdering();

            ordering.OrderList.Count.ShouldBe(2);
            ordering.Skip.ShouldBe(5);
            ordering.Take.ShouldBe(5);
        }
    }
}
