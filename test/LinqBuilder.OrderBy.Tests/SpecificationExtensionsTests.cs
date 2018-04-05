using System;
using System.Linq;
using LinqBuilder.OrderBy.Tests.TestHelpers;
using Shouldly;
using Xunit;

namespace LinqBuilder.OrderBy.Tests
{
    public class SpecificationExtensionsTests
    {
        private readonly Fixture _fixture;

        public SpecificationExtensionsTests()
        {
            _fixture = new Fixture();
            _fixture.AddToCollection(3, 1, 1);
            _fixture.AddToCollection(1, 1, 1);
            _fixture.AddToCollection(2, 1, 1);
        }

        [Fact]
        public void OrderBy_IQueryable_ShouldReturnFilteredAndOrderedList()
        {
            var specification = new Specification<Entity>()
                .OrderBy(new Value1OrderSpecification());

            var result = specification.Invoke(_fixture.Query).ToList();

            result[0].Value1.ShouldBe(1);
            result[1].Value1.ShouldBe(2);
            result[2].Value1.ShouldBe(3);
        }

        [Fact]
        public void OrderBy_IEnumerable_ShouldReturnFilteredAndOrderedList()
        {
            var specification = new Specification<Entity>()
                .OrderBy(new Value1OrderSpecification());

            var result = specification.Invoke(_fixture.Collection).ToList();

            result[0].Value1.ShouldBe(1);
            result[1].Value1.ShouldBe(2);
            result[2].Value1.ShouldBe(3);
        }

        [Fact]
        public void OrderBy_Null_ShouldThrowArgumentNullException()
        {
            Should.Throw<ArgumentNullException>(() => new Specification<Entity>().OrderBy(null));
        }
    }
}
