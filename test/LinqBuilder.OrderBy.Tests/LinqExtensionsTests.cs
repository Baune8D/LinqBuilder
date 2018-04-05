using System;
using System.Linq;
using LinqBuilder.OrderBy.Tests.TestHelpers;
using Shouldly;
using Xunit;

namespace LinqBuilder.OrderBy.Tests
{
    public class LinqExtensionsTests
    {
        private readonly Fixture _fixture;

        public LinqExtensionsTests()
        {
            _fixture = new Fixture();
            _fixture.AddToCollection(3, 1, 1);
            _fixture.AddToCollection(1, 1, 1);
            _fixture.AddToCollection(2, 1, 1);
        }

        [Fact]
        public void OrderBy_IQueryable_ShouldReturnOrderedList()
        {
            var result = _fixture.Query
                .OrderBy(new Value1OrderSpecification())
                .ToList();

            result[0].Value1.ShouldBe(1);
            result[1].Value1.ShouldBe(2);
            result[2].Value1.ShouldBe(3);
        }

        [Fact]
        public void OrderBy_IQueryable_ShouldThrowArgumentNullException()
        {
            Should.Throw<ArgumentNullException>(() => _fixture.Query.OrderBy(null));
        }

        [Fact]
        public void OrderBy_IEnumerable_ShouldReturnOrderedList()
        {
            var result = _fixture.Collection
                .OrderBy(new Value1OrderSpecification())
                .ToList();

            result[0].Value1.ShouldBe(1);
            result[1].Value1.ShouldBe(2);
            result[2].Value1.ShouldBe(3);
        }

        [Fact]
        public void OrderBy_IEnumerable_ShouldThrowArgumentNullException()
        {
            Should.Throw<ArgumentNullException>(() => _fixture.Collection.OrderBy(null));
        }

        [Fact]
        public void ThenBy_IQueryable_ShouldReturnOrderedList()
        {
            var result = _fixture.Query
                .OrderBy(e => e.Value2)
                .ThenBy(new Value1OrderSpecification())
                .ToList();

            result[0].Value1.ShouldBe(1);
            result[1].Value1.ShouldBe(2);
            result[2].Value1.ShouldBe(3);
        }

        [Fact]
        public void ThenBy_IQueryable_ShouldThrowArgumentNullException()
        {
            Should.Throw<ArgumentNullException>(() => _fixture.Query.OrderBy(e => 1).ThenBy(null));
        }

        [Fact]
        public void ThenBy_IEnumerable_ShouldReturnOrderedList()
        {
            var result = _fixture.Collection
                .OrderBy(e => e.Value2)
                .ThenBy(new Value1OrderSpecification())
                .ToList();

            result[0].Value1.ShouldBe(1);
            result[1].Value1.ShouldBe(2);
            result[2].Value1.ShouldBe(3);
        }

        [Fact]
        public void ThenBy_IEnumerable_ShouldThrowArgumentNullException()
        {
            Should.Throw<ArgumentNullException>(() => _fixture.Collection.OrderBy(e => 1).ThenBy(null));
        }

        [Fact]
        public void ExeQuery_IQueryable_ShouldReturnFilteredAndOrderedList()
        {
            var specification = new Specification<Entity>()
                .OrderBy(new Value1OrderSpecification());

            var result = _fixture.Query
                .ExeQuery(specification)
                .ToList();

            result[0].Value1.ShouldBe(1);
            result[1].Value1.ShouldBe(2);
            result[2].Value1.ShouldBe(3);
        }

        [Fact]
        public void ExeQuery_IQueryable_ShouldThrowArgumentNullException()
        {
            Should.Throw<ArgumentNullException>(() => _fixture.Query.ExeQuery(null));
        }

        [Fact]
        public void ExeQuery_IEnumerable_ShouldReturnFilteredAndOrderedList()
        {
            var specification = new Specification<Entity>()
                .OrderBy(new Value1OrderSpecification());

            var result = _fixture.Collection
                .ExeQuery(specification)
                .ToList();

            result[0].Value1.ShouldBe(1);
            result[1].Value1.ShouldBe(2);
            result[2].Value1.ShouldBe(3);
        }

        [Fact]
        public void ExeQuery_IEnumerable_ShouldThrowArgumentNullException()
        {
            Should.Throw<ArgumentNullException>(() => _fixture.Collection.ExeQuery(null));
        }
    }
}
