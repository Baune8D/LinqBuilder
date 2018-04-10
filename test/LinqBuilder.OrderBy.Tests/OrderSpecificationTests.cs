using System;
using System.Linq;
using LinqBuilder.OrderBy.Tests.TestHelpers;
using Shouldly;
using Xunit;

namespace LinqBuilder.OrderBy.Tests
{
    public class OrderSpecificationTests
    {
        private readonly Fixture _fixture;

        public OrderSpecificationTests()
        {
            _fixture = new Fixture();
            _fixture.AddToCollection(3, 1, 1);
            _fixture.AddToCollection(1, 1, 1);
            _fixture.AddToCollection(2, 1, 1);
        }

        [Fact]
        public void Use_OrderSpecification_ShouldBeSameClass()
        {
            var specification = new Value1OrderSpecification();
            specification.AsInterface().ShouldBe(specification);
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
            var specification = new Value1OrderSpecification(Sort.Descending);

            var result = specification.Invoke(_fixture.Query).ToList();

            result[0].Value1.ShouldBe(3);
            result[1].Value1.ShouldBe(2);
            result[2].Value1.ShouldBe(1);
        }

        [Fact]
        public void Invoke_IEnumerableDescending_ShouldReturnOrderedList()
        {
            var specification = new Value1OrderSpecification(Sort.Descending);

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

        [Fact]
        public void Paginate_PageNoAndSize_ShouldReturnCorrectOrdering()
        {
            var specification = new Value1OrderSpecification().Paginate(2, 5);

            var ordering = specification.GetOrdering();

            ordering.OrderList.Count.ShouldBe(1);
            ordering.Skip.ShouldBe(5);
            ordering.Take.ShouldBe(5);
        }

        [Fact]
        public void Paginate_InvalidPageNo_ShouldThrowArgumentException()
        {
            Should.Throw<ArgumentException>(() => new Value1OrderSpecification().Paginate(0, 5));
        }

        [Fact]
        public void Paginate_InvalidPageSize_ShouldThrowArgumentException()
        {
            Should.Throw<ArgumentException>(() => new Value1OrderSpecification().Paginate(1, 0));
        }
    }
}
