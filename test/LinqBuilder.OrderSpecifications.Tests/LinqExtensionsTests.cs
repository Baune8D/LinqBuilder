using System;
using System.Linq;
using LinqBuilder.OrderSpecifications.Tests.TestHelpers;
using LinqBuilder.Specifications;
using Shouldly;
using Xunit;

namespace LinqBuilder.OrderSpecifications.Tests
{
    public class LinqExtensionsTests : IClassFixture<Fixture>
    {
        private readonly Fixture _fixture;

        public LinqExtensionsTests(Fixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void OrderBy_IQueryable_ShouldReturnOrderedList()
        {
            var result = _fixture.Query.OrderBy(new Value1OrderSpecification()).ToList();

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
            var result = _fixture.Collection.OrderBy(new Value1OrderSpecification()).ToList();

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
            var orderedQuery = _fixture.Query.OrderBy(e => e.Value2);

            var result = orderedQuery.ThenBy(new Value1OrderSpecification()).ToList();

            result[0].Value1.ShouldBe(1);
            result[1].Value1.ShouldBe(2);
            result[2].Value1.ShouldBe(3);
        }

        [Fact]
        public void ThenBy_IQueryable_ShouldThrowArgumentNullException()
        {
            var query = _fixture.Query.OrderBy(e => e.Value2);

            Should.Throw<ArgumentNullException>(() => query.ThenBy(null));
        }

        [Fact]
        public void ThenBy_IEnumerable_ShouldReturnOrderedList()
        {
            var orderedQuery = _fixture.Collection.OrderBy(e => e.Value2);

            var result = orderedQuery.ThenBy(new Value1OrderSpecification()).ToList();

            result[0].Value1.ShouldBe(1);
            result[1].Value1.ShouldBe(2);
            result[2].Value1.ShouldBe(3);
        }

        [Fact]
        public void ThenBy_IEnumerable_ShouldThrowArgumentNullException()
        {
            var collection = _fixture.Collection.OrderBy(e => e.Value2);

            Should.Throw<ArgumentNullException>(() => collection.ThenBy(null));
        }

        [Fact]
        public void ExeSpec_IQueryable_ShouldReturnFilteredAndOrderedList()
        {
            var specification = new Specification<Entity>()
                .OrderBy(new Value1OrderSpecification());

            var result = _fixture.Query.ExeSpec(specification).ToList();

            result[0].Value1.ShouldBe(1);
            result[1].Value1.ShouldBe(2);
            result[2].Value1.ShouldBe(3);
        }

        [Fact]
        public void ExeSpec_IQueryable_ShouldThrowArgumentNullException()
        {
            Should.Throw<ArgumentNullException>(() => _fixture.Query.ExeSpec(null));
        }

        [Fact]
        public void ExeSpec_IEnumerable_ShouldReturnFilteredAndOrderedList()
        {
            var specification = new Specification<Entity>()
                .OrderBy(new Value1OrderSpecification());

            var result = _fixture.Collection.ExeSpec(specification).ToList();

            result[0].Value1.ShouldBe(1);
            result[1].Value1.ShouldBe(2);
            result[2].Value1.ShouldBe(3);
        }

        [Fact]
        public void ExeSpec_IEnumerable_ShouldThrowArgumentNullException()
        {
            Should.Throw<ArgumentNullException>(() => _fixture.Collection.ExeSpec(null));
        }
    }
}
