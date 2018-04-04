using System;
using System.Linq;
using LinqBuilder.Specifications.Tests.TestHelpers;
using Shouldly;
using Xunit;

namespace LinqBuilder.Specifications.Tests
{
    public class LinqExtensionsTests : IClassFixture<Fixture>
    {
        private readonly Fixture _fixture;

        public LinqExtensionsTests(Fixture fixture)
        {
            _fixture = fixture;
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
        public void Any_IQueryable_ShouldReturnBooleanTrue()
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
        public void Any_IEnumerable_ShouldReturnBooleanTrue()
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
        public void All_IQueryable_ShouldReturnBooleanFalse()
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
        public void All_IEnumerable_ShouldReturnBooleanFalse()
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
