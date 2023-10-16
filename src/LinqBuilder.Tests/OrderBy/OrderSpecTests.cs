using System.Linq;
using FluentAssertions;
using LinqBuilder.OrderBy;
using LinqBuilder.Tests.Data;
using Xunit;

namespace LinqBuilder.Tests.OrderBy
{
    public class OrderSpecTests
    {
        private readonly Fixture _fixture;

        public OrderSpecTests()
        {
            _fixture = new Fixture();
            _fixture.AddToCollection(3, 1, 1);
            _fixture.AddToCollection(1, 1, 1);
            _fixture.AddToCollection(2, 1, 1);
        }

        [Fact]
        public void Constructor_InlineExpression__ShouldReturnOrderedList()
        {
            var specification = new OrderSpec<Entity, int>(entity => entity.Value1);
            var result = _fixture.Query.OrderBy(specification).ToList();
            result[0].Value1.Should().Be(1);
            result[1].Value1.Should().Be(2);
            result[2].Value1.Should().Be(3);
        }

        [Fact]
        public void Constructor_InlineExpression_ShouldReturnOrderedList()
        {
            var specification = new OrderSpec<Entity, int>(entity => entity.Value1, Sort.Descending);
            var result = _fixture.Query.OrderBy(specification).ToList();
            result[0].Value1.Should().Be(3);
            result[1].Value1.Should().Be(2);
            result[2].Value1.Should().Be(1);
        }
    }
}
