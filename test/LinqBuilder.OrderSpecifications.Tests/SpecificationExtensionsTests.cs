using System.Linq;
using LinqBuilder.OrderSpecifications.Tests.TestHelpers;
using LinqBuilder.Specifications;
using Shouldly;
using Xunit;

namespace LinqBuilder.OrderSpecifications.Tests
{
    public class SpecificationExtensionsTests : IClassFixture<Fixture>
    {
        private readonly Fixture _fixture;

        public SpecificationExtensionsTests(Fixture fixture)
        {
            _fixture = fixture;
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
    }
}
