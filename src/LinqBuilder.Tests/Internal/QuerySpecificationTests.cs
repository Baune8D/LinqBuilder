using LinqBuilder.Testing;
using Shouldly;
using Xunit;

namespace LinqBuilder.Tests.Internal
{
    public class QuerySpecificationTests
    {
        [Fact]
        public void AsFunc_NoExpression_ShouldBeNull()
        {
            new Specification<Entity>().AsFunc().ShouldBeNull();
        }
    }
}
