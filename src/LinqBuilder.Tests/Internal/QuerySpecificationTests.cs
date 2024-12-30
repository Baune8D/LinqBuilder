using FluentAssertions;
using LinqBuilder.Tests.Data;
using Xunit;

namespace LinqBuilder.Tests.Internal;

public class QuerySpecificationTests
{
    [Fact]
    public void AsFunc_NoExpression_ShouldBeNull()
    {
        new Specification<Entity>().AsFunc().Should().BeNull();
    }
}
