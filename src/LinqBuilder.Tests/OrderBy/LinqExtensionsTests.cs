using System.Linq;
using FluentAssertions;
using LinqBuilder.OrderBy;
using LinqBuilder.Tests.Data;
using Xunit;

namespace LinqBuilder.Tests.OrderBy;

public class LinqExtensionsTests
{
    private readonly IOrderSpecification<Entity> _orderValue1Asc = OrderSpec<Entity, int>.New(entity => entity.Value1);
    private readonly IOrderSpecification<Entity> _orderValue2Asc = OrderSpec<Entity, int>.New(entity => entity.Value2);

    private readonly Fixture _fixture;

    public LinqExtensionsTests()
    {
        _fixture = new Fixture();
        _fixture.AddToCollection(3, 1, 1);
        _fixture.AddToCollection(1, 1, 1);
        _fixture.AddToCollection(2, 2, 1);
        _fixture.AddToCollection(2, 1, 1);
    }

    [Fact]
    public void OrderBy_IQueryable_ShouldReturnCorrectResult()
    {
        var result = _fixture.Query
            .OrderBy(_orderValue1Asc)
            .ToList();

        result.Count.Should().Be(4);
        result[0].Value1.Should().Be(1);
        result[1].Value1.Should().Be(2);
        result[1].Value2.Should().Be(2);
        result[2].Value1.Should().Be(2);
        result[2].Value2.Should().Be(1);
        result[3].Value1.Should().Be(3);
    }

    [Fact]
    public void OrderBy_IEnumerable_ShouldReturnCorrectResult()
    {
        var result = _fixture.Collection
            .OrderBy(_orderValue1Asc)
            .ToList();

        result.Count.Should().Be(4);
        result[0].Value1.Should().Be(1);
        result[1].Value1.Should().Be(2);
        result[1].Value2.Should().Be(2);
        result[2].Value1.Should().Be(2);
        result[2].Value2.Should().Be(1);
        result[3].Value1.Should().Be(3);
    }

    [Fact]
    public void ThenBy_IQueryable_ShouldReturnCorrectResult()
    {
        var result = _fixture.Query
            .OrderBy(_orderValue1Asc)
            .ThenBy(_orderValue2Asc)
            .ToList();

        result.Count.Should().Be(4);
        result[0].Value1.Should().Be(1);
        result[1].Value1.Should().Be(2);
        result[1].Value2.Should().Be(1);
        result[2].Value1.Should().Be(2);
        result[2].Value2.Should().Be(2);
        result[3].Value1.Should().Be(3);
    }

    [Fact]
    public void ThenBy_IEnumerable_ShouldReturnCorrectResult()
    {
        var result = _fixture.Collection
            .OrderBy(_orderValue1Asc)
            .ThenBy(_orderValue2Asc)
            .ToList();

        result.Count.Should().Be(4);
        result[0].Value1.Should().Be(1);
        result[1].Value1.Should().Be(2);
        result[1].Value2.Should().Be(1);
        result[2].Value1.Should().Be(2);
        result[2].Value2.Should().Be(2);
        result[3].Value1.Should().Be(3);
    }
}
