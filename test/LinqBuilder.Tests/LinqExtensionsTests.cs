using System;
using System.Linq;
using LinqBuilder.Tests.TestHelpers;
using Shouldly;
using Xunit;

namespace LinqBuilder.Tests
{
    public class LinqExtensionsTests
    {
        private readonly Fixture _fixture;

        public LinqExtensionsTests()
        {
            _fixture = new Fixture();
            _fixture.AddToCollection(3, 1);
            _fixture.AddToCollection(3, 1);
            _fixture.AddToCollection(1, 1);
        }

        [Fact]
        public void Where_IQueryable_ShouldReturnFilteredQueryable()
        {
            var result = _fixture.Query.Where(_fixture.Specification);

            result.ShouldBeAssignableTo<IQueryable<Entity>>();
            result.ShouldAllBe(e => e.Value1 == _fixture.Value);
        }

        [Fact]
        public void Where_IQueryable_ShouldThrowArgumentNullException()
        {
            Should.Throw<ArgumentNullException>(() => _fixture.Query.Where(null));
        }

        [Fact]
        public void Where_IEnumerable_ShouldReturnFilteredEnumerable()
        {
            var result = _fixture.Collection.Where(_fixture.Specification);

            result.ShouldNotBeAssignableTo<IQueryable<Entity>>();
            result.ShouldAllBe(e => e.Value1 == _fixture.Value);
        }

        [Fact]
        public void Where_IEnumerable_ShouldThrowArgumentNullException()
        {
            Should.Throw<ArgumentNullException>(() => _fixture.Collection.Where(null));
        }

        [Fact]
        public void Any_IQueryable_ShouldBeTrue()
        {
            var result = _fixture.Query.Any(_fixture.Specification);

            result.ShouldBeTrue();
        }

        [Fact]
        public void Any_IQueryable_ShouldThrowArgumentNullException()
        {
            Should.Throw<ArgumentNullException>(() => _fixture.Query.Any(null));
        }

        [Fact]
        public void Any_IEnumerable_ShouldBeTrue()
        {
            var result = _fixture.Collection.Any(_fixture.Specification);

            result.ShouldBeTrue();
        }

        [Fact]
        public void Any_IEnumerable_ShouldThrowArgumentNullException()
        {
            Should.Throw<ArgumentNullException>(() => _fixture.Collection.Any(null));
        }

        [Fact]
        public void All_IQueryable_ShouldBeFalse()
        {
            var result = _fixture.Query.All(_fixture.Specification);

            result.ShouldBeFalse();
        }

        [Fact]
        public void All_IQueryable_ShouldThrowArgumentNullException()
        {
            Should.Throw<ArgumentNullException>(() => _fixture.Query.All(null));
        }

        [Fact]
        public void All_IEnumerable_ShouldBeFalse()
        {
            var result = _fixture.Collection.All(_fixture.Specification);

            result.ShouldBeFalse();
        }

        [Fact]
        public void All_IEnumerable_ShouldThrowArgumentNullException()
        {
            Should.Throw<ArgumentNullException>(() => _fixture.Collection.All(null));
        }
    }
}
