using System;
using System.Linq;
using LinqBuilder.Specifications.Tests.TestHelpers;
using Shouldly;
using Xunit;

namespace LinqBuilder.Specifications.Tests
{
    public class LinqExtensionsTests : IClassFixture<TestFixture>
    {
        private readonly TestFixture _fixture;

        public LinqExtensionsTests(TestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Where_IQueryable_ShouldReturnFilteredQueryable()
        {
            var result = _fixture.TestQuery.Where(_fixture.Specification);

            result.ShouldBeAssignableTo<IQueryable<TestEntity>>();
            result.ShouldAllBe(e => e.Value1 == _fixture.Value);
        }

        [Fact]
        public void Where_IQueryable_ShouldThrowArgumentNullException()
        {
            Should.Throw<ArgumentNullException>(() => _fixture.TestQuery.Where(null));
        }

        [Fact]
        public void Where_IEnumerable_ShouldReturnFilteredEnumerable()
        {
            var result = _fixture.TestCollection.Where(_fixture.Specification);

            result.ShouldNotBeAssignableTo<IQueryable<TestEntity>>();
            result.ShouldAllBe(e => e.Value1 == _fixture.Value);
        }

        [Fact]
        public void Where_IEnumerable_ShouldThrowArgumentNullException()
        {
            Should.Throw<ArgumentNullException>(() => _fixture.TestCollection.Where(null));
        }
    }
}
