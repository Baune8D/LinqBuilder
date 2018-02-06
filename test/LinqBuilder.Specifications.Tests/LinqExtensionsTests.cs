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
    }
}
